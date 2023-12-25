using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using QDiet.Api.Properties;
using QDiet.Domain.Logging;
using QDiet.Domain.Models.Auth;
using QDiet.Domain.Models.DataBase;
using QDiet.Domain.Service;
using QDiet.Domain.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace QDiet.Api.Controllers
{
    public class AuthController : AbstractController
    {

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthModel authModel)
        {
            User user = await DbService.GetUserAsync(authModel);

            if (user == null)
            {
                return Unauthorized(new { Comment = "Неверный логин или пароль." });
            }

            List<Claim>? authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Id", user.Id.ToString())
            };

            foreach (Role role in user.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            SecurityToken? token = CreateToken(authClaims);
            string? refreshToken = GenerateRefreshToken();

            await DbService.UpdateUserRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expire = token.ValidTo
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel regModel)
        {
            RegisterValidator validations = new RegisterValidator();
            ValidationResult result = validations.Validate(regModel);

            if (!result.IsValid)
            {
                StringBuilder problem = new StringBuilder();

                problem.AppendLine("Ошибка валидации:");

                foreach (ValidationFailure error in result.Errors)
                {
                    problem.AppendLine(error.ErrorMessage);
                }

                return BadRequest(new { Comment = problem });
            }

            if (await DbService.UserNameExistAsync(regModel.UserName))
            {
                return BadRequest(new { Comment = "Пользователь с данным логином уже существует." });
            }

            User? user = await DbService.AddUserAsync(regModel);

            if (user == null)
            {
                return StatusCode(500, "Не удалось зарегистрировать пользователя.");
            }

            return Created();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest(new { Comment = "Неизвестный запрос от клиента." });
            }

            ClaimsIdentity? identity = await GetIdentityFromExpiredToken(tokenModel.AccessToken);

            if (identity == null)
            {
                return BadRequest(new { Comment = "Неверный токен или refresh токен." });
            }

            User? user = await DbService.GetUserAsync(identity.Name);

            if (user == null ||
                !user.RefreshToken.Equals(tokenModel.RefreshToken) ||
                user.RefreshTokenExpireTime <= DateTime.Now)
            {
                return BadRequest(new { Comment = "Неверный токен или refresh токен." });
            }

            SecurityToken? newAccessToken = CreateToken(identity.Claims);
            string? newRefreshToken = GenerateRefreshToken();

            await DbService.UpdateUserRefreshTokenAsync(user, newRefreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }

        private SecurityToken? CreateToken(IEnumerable<Claim> authClaims)
        {
            byte[]? key = Encoding.ASCII.GetBytes(Resources.JwtTokenKey);

            return new JwtSecurityToken(issuer: Resources.ValideIssuer,
                                        audience: null,
                                        claims: authClaims,
                                        expires: DateTime.UtcNow.AddMinutes(30),
                                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
        }

        private static string GenerateRefreshToken()
        {
            byte[]? randomNumber = new byte[64];
            using RandomNumberGenerator? rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async static Task<ClaimsIdentity?> GetIdentityFromExpiredToken(string? token)
        {
            TokenValidationParameters? tokenValidationParamenters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Resources.JwtTokenKey)),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationResult? principal = await tokenHandler.ValidateTokenAsync(token, tokenValidationParamenters);

            if (!principal.IsValid)
            {
                throw new SecurityTokenException("Неизвестный токен.");
            }

            return principal.ClaimsIdentity;
        }
    }
}