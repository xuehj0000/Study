using Microsoft.AspNetCore.Mvc;

namespace StudyDemo4_UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
