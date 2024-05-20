using BnFurniture.Application.Controllers.ProductCategoryController;
using BnFurniture.Application.Controllers.ProductCategoryController.Commands;
using BnFurniture.Application.Controllers.ProductCategoryController.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurnitureApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromServices] AddProductCategoryHandler handler,
            [FromBody] ProductCategoryDTO model)
        {
            var command = new ProductCategoryCommand(model);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCategories([FromServices] GetProductCategoriesHandler handler)
        {
            var command = new NoParameters();
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromServices] DeleteProductCategoryHandler handler, Guid categoryId)
        {
            var command = new DeleteProductCategoryCommand(categoryId);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromServices] UpdateProductCategoryHandler handler,
             [FromBody] ProductCategoryDTO updatedCategory)
        {
            var command = new UpdateProductCategoryCommand(updatedCategory);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
