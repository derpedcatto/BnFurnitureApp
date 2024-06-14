using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.CategoryController.Queries;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategories(
        [FromServices] GetAllCategoriesHandler handler,
        [FromQuery] bool includeImages = true,
        [FromQuery] bool flatList = false,
        [FromQuery] bool randomOrder = false,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
    {
        var query = new GetAllCategoriesQuery(
            includeImages,
            flatList,
            randomOrder,
            pageNumber,
            pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("all-with-types")]
    public async Task<IActionResult> GetAllCategoriesWithProductTypes(
        [FromServices] GetAllCategoriesWithProductTypesHandler handler,
        [FromQuery] bool includeImages = true)
    {
        var query = new GetAllCategoriesWithProductTypesQuery(includeImages);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{categoryId:guid}/types")]
    public async Task<IActionResult> GetCategoryTypes(
        [FromServices] GetCategoryTypesHandler handler,
        Guid categoryId,
        [FromQuery] bool includeImages = true)
    {
        var query = new GetCategoryTypesQuery(categoryId, includeImages);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
