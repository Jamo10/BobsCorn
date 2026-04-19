using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BobsCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CornController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get()
        {
            return Ok("Success");
        }
    }
}
