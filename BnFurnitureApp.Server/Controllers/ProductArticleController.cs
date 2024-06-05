using BnFurniture.Application.Controllers.ProductArticleController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

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
}
