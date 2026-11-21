using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "success",
                message = "Backend ASP.NET Core is running",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
