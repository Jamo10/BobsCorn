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

        public ClientCornController(IFarm farm)
        {
            _farm = farm;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string clientName)
        {
            var cordDto = new DTO.CornDto
            {
                Code = 200,
                Message = "Success"
            };

            cordDto.TotalClient = await _farm.GetTotalClientCornAsync(clientName);

            return Ok(cordDto);
        }
    }
}
