using Microsoft.AspNetCore.Mvc;

namespace QDiet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController : ControllerBase
    {
        protected ILogger _logger;

        public AbstractController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
