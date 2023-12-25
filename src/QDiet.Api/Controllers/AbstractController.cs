using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using QDiet.Domain.Models.DataBase;
using QDiet.Domain.Service.DataBase;

namespace QDiet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController : ControllerBase
    {
        protected async Task<User> GetUser()
        {
            return await DbUserService.GetUserAsync(long.Parse(User.Claims.First(claim => claim.Type.Equals("Id")).Value));
        }
    }
}
