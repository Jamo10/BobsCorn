using BobsCorn.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BobsCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientCornController : ControllerBase
    {
        private readonly IFarm _farm;
        private readonly ILogger<ClientCornController> _logger;

        public ClientCornController(IFarm farm, ILogger<ClientCornController> logger)
        {
            _farm = farm;
            _logger = logger;
        }

        /// <summary>
        /// Gets the total amount of corn bought by a specific client.
        /// </summary>
        /// <param name="clientName">The name of the client.</param>
        /// <returns>Returns the total of corn bought by a client</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string clientName)
        {
            var cordDto = new DTO.CornDto
            {
                Code = 200,
                Message = "Success"
            };

            try
            {
                cordDto.TotalClient = await _farm.GetTotalClientCornAsync(clientName);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving total client corn.");
                cordDto.Code = 500;
                cordDto.Message = "An error occurred while retrieving the total client corn.";
                return StatusCode(500, cordDto);
            }


            return Ok(cordDto);
        }
    }
}
