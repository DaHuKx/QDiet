using Microsoft.EntityFrameworkCore;
using QDiet.Domain.DataBase;
using QDiet.Domain.Models.Auth;
using QDiet.Domain.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QDiet.Domain.Service
{
    /// <summary>
    /// Сервис базы данных
    /// </summary>
    public static class DbService
    {
        /// <summary>
        /// Получение пользователя по логину и паролю
        /// </summary>
        /// <param name="model">Модель аутентификации, которая содержит логин и пароль</param>
        /// <returns></returns>
        public async static Task<User> GetUserAsync(AuthModel model)
        {
            using (Db db = new Db())
            {
                return await db.Users.Include(u => u.Roles)
                                     .FirstOrDefaultAsync(user => user.Username.Equals(model.UserName) &&
                                                                               user.Password.Equals(HashString(model.Password)));
            }
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public async static Task<User> GetUserAsync(long id)
        {
            using (Db db = new Db())
            {
                return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <returns></returns>
        public async static Task<User> GetUserAsync(string userName)
        {
            using (Db db = new Db())
            {
                return await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower()
                                                                                      .Equals(userName.ToLower()));
            }
        }

        /// <summary>
        /// Добавление нового пользователя в базу данных.
        /// </summary>
        /// <param name="registerModel">Модель регистрации пользоваетля</param>
        /// <returns></returns>
        public async static Task<User> AddUserAsync(RegisterModel registerModel)
        {
            User user = new User()
            {
                Username = registerModel.UserName,
                Password = HashString(registerModel.Password),
                Email = registerModel.Email,
            };

            using (Db db = new Db())
            {
                Role role = await db.Roles.FirstAsync(role => role.Name.Equals("User"));

                user.Roles = new List<Role>() { role };

                await db.Users.AddAsync(user);

                try
                {
                    await db.SaveChangesAsync();

                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Проверка, существует ли логин в базе данных
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <returns></returns>
        public async static Task<bool> UserNameExistAsync(string userName)
        {
            using (Db db = new Db())
            {
                return await db.Users.AnyAsync(u => u.Username.ToLower()
                                                                           .Equals(userName.Trim()
                                                                                                 .ToLower()));
            }
        }

        /// <summary>
        /// Обновление Refresh токена пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="refreshToken">Refresh токен</param>
        /// <param name="expireDate">Дата окончания действия Refresh токена</param>
        /// <returns></returns>
        public async static Task<User> UpdateUserRefreshTokenAsync(User user, string refreshToken, DateTime expireDate)
        {
            using (Db db = new Db())
            {
                User dbUser = db.Users.First(u => u.Id == user.Id);

                dbUser.RefreshToken = refreshToken;
                dbUser.RefreshTokenExpireTime = expireDate.ToUniversalTime();
                
                await db.SaveChangesAsync();
            }

            return user;
        }

        /// <summary>
        /// Получение хэшированной строки
        /// </summary>
        /// <param name="str">Строка для хэширования</param>
        /// <returns></returns>
        private static string HashString(string str)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(Encoding.ASCII.GetBytes(str));
                return Convert.ToHexString(hashBytes);
            }
        }


    }
}
