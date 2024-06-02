using BnFurniture.Application.Controllers.CharacteristicValueController.Commands;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Controllers.CharacteristicValueController.Queries;
using BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCharacteristicValueController : ControllerBase
    {
        private readonly ILogger<ProductCharacteristicValueController> _logger;

        public ProductCharacteristicValueController(ILogger<ProductCharacteristicValueController> logger)
        {
            _logger = logger;
        }

        [HttpPost("characteristic-values")]
        public async Task<IActionResult> CreateCharacteristicValue([FromServices] CreateCharacteristicValueHandler handler, [FromBody] CreateCharacteristicValueDTO dto)
        {
            _logger.LogInformation("CreateCharacteristicValue called with DTO: {@dto}", dto);
            var command = new CreateCharacteristicValueCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut("characteristic-values")]
        public async Task<IActionResult> UpdateCharacteristicValue([FromServices] UpdateCharacteristicValueHandler handler, [FromBody] UpdateCharacteristicValueDTO dto)
        {
            _logger.LogInformation("UpdateCharacteristicValue called with DTO: {@dto}", dto);
            var command = new UpdateCharacteristicValueCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet("characteristic-values")]
        public async Task<IActionResult> GetAllCharacteristicValues([FromServices] GetAllCharacteristicValuesHandler handler)
        {
            var query = new GetAllCharacteristicValuesQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("characteristic-values/{id}")]
        public async Task<IActionResult> DeleteCharacteristicValue([FromServices] DeleteCharacteristicValueHandler handler, Guid id)
        {
            _logger.LogInformation("DeleteCharacteristicValue called with ID: {id}", id);
            var command = new DeleteCharacteristicValueCommand(id);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
