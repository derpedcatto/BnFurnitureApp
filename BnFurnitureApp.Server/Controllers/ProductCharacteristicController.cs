using BnFurniture.Application.Controllers.CharacteristicController.DTO;
using BnFurniture.Application.Controllers.CharacteristicController.Queries;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicConfiguration.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Application.Controllers.ProductController.Queries;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Application.Controllers.ProductCharacteristicController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCharacteristicController : ControllerBase
    {
        [HttpGet("characteristic/{slug}")]
        public async Task<IActionResult> GetCharacteristic([FromServices] GetCharacteristicHandler handler, string slug)
        {
            var query = new GetCharacteristicQuery(slug);
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet("product/{slug}")]
        public async Task<IActionResult> GetProductWithCharacteristics([FromServices] GetProductWithCharacteristicsHandler handler, string slug)
        {
            var query = new GetProductWithCharacteristicsQuery(slug);
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
        [HttpPost("characteristics")]
        public async Task<IActionResult> CreateCharacteristic([FromServices] CreateCharacteristicHandler handler, [FromBody] CreateCharacteristicDTO dto)
        {
            var command = new CreateCharacteristicCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPost("characteristic-values")]
        public async Task<IActionResult> CreateCharacteristicValue([FromServices] CreateCharacteristicValueHandler handler, [FromBody] CreateCharacteristicValueDTO dto)
        {
            var command = new CreateCharacteristicValueCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPost("product-characteristic-configuration")]
        public async Task<IActionResult> CreateProductCharacteristicConfiguration([FromServices] CreateProductCharacteristicConfigurationHandler handler, [FromBody] CreateProductCharacteristicConfigurationDTO dto)
        {
            var command = new CreateProductCharacteristicConfigurationCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }


    }
}
