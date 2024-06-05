using BnFurniture.Application.Controllers.ProductArticleController.Commands;
using BnFurniture.Application.Controllers.ProductArticleController.DTO;
using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Application.Controllers.ProductArticleController;

[Route("api/[controller]")]
[ApiController]
public class ProductArticleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductArticles([FromServices] GetAllProductArticlesHandler handler)
    {
        var query = new GetAllProductArticlesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductArticle([FromServices] CreateProductArticleHandler handler,
                                                          [FromBody] CreateProductArticleDTO model)
    {
        var command = new CreateProductArticleCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductArticle([FromServices] UpdateProductArticleHandler handler,
   [FromBody] UpdateProductArticleDTO model)
    {
        var command = new UpdateProductArticleCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{articleId:guid}")]
    public async Task<IActionResult> DeleteProductArticle([FromServices] DeleteProductArticleHandler handler,
       Guid articleId)
    {
        var command = new DeleteProductArticleCommand(articleId);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
