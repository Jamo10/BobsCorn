using BobsCorn.Repository;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;

namespace BobsCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("one-per-minute-per-client")]
    public class CornController : ControllerBase
    {
        private readonly IFarm _farm;

        public CornController(IFarm farm) {
            _farm = farm;
        }
        
        [HttpPost]
        public async Task<IActionResult> Get()
        {
            var clientName = Request.Headers["Client-Name"].FirstOrDefault();

            if (String.IsNullOrEmpty(clientName)) 
                return BadRequest(new { Code = 400, Message = "Client-Name header is required." });

            var cordDto = new DTO.CornDto
            {
                Code = 200,
                Message = "Success"
            };

            cordDto.Quantity = await _farm.GetCornAsync();

            if (cordDto.Quantity < 0)
            {
                cordDto.Code = 409;
                cordDto.Message = "No more corn.";

                return Conflict(cordDto);
            }

            cordDto.TotalClient = await _farm.UpdateClientCornAsync(clientName);

            return Ok(cordDto);
        }
    }
}
