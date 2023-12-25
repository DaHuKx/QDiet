using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using QDiet.Domain.Logging;
using QDiet.Domain.Models.DataBase;
using QDiet.Domain.Service;

namespace QDiet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController : ControllerBase
    {
        protected async Task<User> GetUser()
        {
            return await DbService.GetUserAsync(long.Parse(User.Claims.First(claim => claim.Type.Equals("Id")).Value));
        }
    }
}
