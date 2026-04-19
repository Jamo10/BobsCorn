using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BobsCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("one-per-minute-per-client")]
    public class CornController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get()
        {
            var cordDto = new DTO.CornDto
            {
                Code = 200,
                Message = "Success"
            };

            return Ok(cordDto);
        }
    }
}
