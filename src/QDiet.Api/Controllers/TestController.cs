using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QDiet.Api.Controllers
{
    public class TestController : AbstractController
    {
        public class MathTemplate
        {
            public int FirstNumber { get; set; }
            public int SecondNumber { get; set; }
        }

        [HttpPost("SummWithoutAuthorize")]
        public IActionResult SummWithoutAuthorize([FromBody] MathTemplate template)
        {
            

            return Ok($"Answer is: {template.FirstNumber + template.SecondNumber}");
        }

        [Authorize]
        [HttpPost("MultiplyWithAuthorization")]
        public IActionResult MultiplyWithAuthorization([FromBody] MathTemplate template)
        {
            return Ok($"Answer is: {template.FirstNumber * template.SecondNumber}");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ModForAdmin")]
        public IActionResult ModForAdmin([FromBody] MathTemplate template)
        {
            return Ok($"Answer is: {template.FirstNumber % template.SecondNumber}");
        }
    }
}
