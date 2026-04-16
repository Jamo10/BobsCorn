using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BobsCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CornController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");
        }
    }
}
