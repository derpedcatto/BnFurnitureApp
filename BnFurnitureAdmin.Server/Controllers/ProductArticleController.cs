using BnFurniture.Application.Controllers.ProductArticleController.Commands;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Application.Controllers.ProductArticleController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductArticleController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateProductArticle([FromServices] CreateProductArticleHandler handler,
                                                              [FromBody] CreateProductArticleDTO model)
        {
            var command = new CreateProductArticleCommand(model);
            var apiResponse = await handler.Handle(command, CancellationToken.None);
            return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
        }
    }
}
