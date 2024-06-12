using BnFurniture.Application.Controllers.ProductReviewController.Commands;
using BnFurniture.Application.Controllers.ProductReviewController.DTO;
using BnFurniture.Application.Controllers.ProductReviewController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductReview([FromServices] CreateProductReviewHandler handler, [FromBody] CreateProductReviewDTO dto)
        {
            var command = new CreateProductReviewCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductReview([FromServices] UpdateProductReviewHandler handler, [FromBody] UpdateProductReviewDTO dto)
        {
            var command = new UpdateProductReviewCommand(dto);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview([FromServices] DeleteProductReviewHandler handler, Guid id)
        {
            var command = new DeleteProductReviewCommand(id);
            var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductReviews([FromServices] GetAllProductReviewsHandler handler)
        {
            var query = new GetAllProductReviewsQuery();
            var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}