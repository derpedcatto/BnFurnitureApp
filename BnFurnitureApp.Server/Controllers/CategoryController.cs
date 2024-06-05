using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.CategoryController.Queries;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategories([FromServices] GetAllCategoriesHandler handler)
    {
        var query = new GetAllCategoriesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("all-with-types")]
    public async Task<IActionResult> GetAllCategoriesWithProductTypes([FromServices] GetAllCategoriesWithProductTypesHandler handler)
    {
        var query = new GetAllCategoriesWithProductTypesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
