using BnFurniture.Application.Controllers.ProductCategoryController.Commands;
using BnFurniture.Application.Controllers.ProductController.Commands;
using BnFurniture.Application.Controllers.ProductController.DTO;
using Microsoft.AspNetCore.Mvc;
using static BnFurniture.Application.Controllers.ProductController.Commands.CreateProductHandler;

namespace BnFurnitureApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromServices] CreateProductHandler handler,
            [FromBody] ProductDTO model)
        {
            var command = new CreateProductCommand(model);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllProducts([FromServices] GetAllProductsHandler handler)
        {
            var command = new NoParameters();
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct([FromServices] DeleteProductHandler handler, Guid id)
        {
            var command = new DeleteProductCommand(id);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromServices] UpdateProductHandler handler,
            [FromBody] ProductDTO updatedProduct)
        {
            var command = new UpdateProductCommand(updatedProduct);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
