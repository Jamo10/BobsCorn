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
        private readonly ILogger<CornController> _logger;

        public CornController(IFarm farm, ILogger<CornController> logger) {
            _farm = farm;
            _logger = logger;
        }
        
        /// <summary>
        /// Handles a POST request to retrieve the current corn quantity and update the client record based on the
        /// 'Client-Name' header.
        /// </summary>
        /// <remarks>The 'Client-Name' header must be provided in the request. The response includes
        /// status codes and messages indicating the result of the operation.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a DTO with the corn quantity and client information if successful;
        /// a 400 Bad Request result if the 'Client-Name' header is missing; or a 409 Conflict result if no corn is available.</returns>
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

            try
            {
                cordDto.Quantity = await _farm.GetCornAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving corn quantity.");
                cordDto.Code = 500;
                cordDto.Message = "An error occurred while retrieving corn quantity.";
                return StatusCode(500, cordDto);
            }

            if (cordDto.Quantity < 0)
            {
                cordDto.Code = 409;
                cordDto.Message = "No more corn.";

                return Conflict(cordDto);
            }

            try
            {
                cordDto.TotalClient = await _farm.UpdateClientCornAsync(clientName);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating client corn.");
                cordDto.Code = 500;
                cordDto.Message = "An error occurred while updating client corn.";
                return StatusCode(500, cordDto);
            }

            return Ok(cordDto);
        }
    }
}
