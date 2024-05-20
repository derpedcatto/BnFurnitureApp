using BnFurniture.Application.Controllers.ProductCategoryController.Commands;
using BnFurniture.Application.Controllers.ProductTypeController.Commands;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurnitureApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddProductType([FromServices] CreateProductTypeHandler handler,
            [FromBody] ProductTypeDTO model)
        {
            var command = new CreateProductTypeCommand(model);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllProductTypes([FromServices] GetAllProductTypesHandler handler)
        {
            var command = new NoParameters();
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductType([FromServices] DeleteProductTypeHandler handler, Guid id)
        {
            var command = new DeleteProductTypeCommand(id);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductType([FromServices] UpdateProductTypeHandler handler,
            [FromBody] ProductTypeDTO updatedProductType)
        {
            var command = new UpdateProductTypeCommand(updatedProductType);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
