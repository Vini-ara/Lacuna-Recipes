using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    [Route("api/healthCheck")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Healthy");
        }
    }
}
