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

    [HttpGet("{categorySlug}")]
    public async Task<IActionResult> GetAllSubCategories(
        [FromServices] GetAllSubCategoriesHandler handler,
        string categorySlug,
        [FromQuery] bool includeImages = true,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
    {
        var query = new GetAllSubCategoriesQuery(
            categorySlug,
            includeImages,
            pageNumber,
            pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("all-with-types")]
    public async Task<IActionResult> GetAllCategoriesWithProductTypes(
        [FromServices] GetAllCategoriesWithProductTypesHandler handler,
        [FromQuery] bool includeImages = true,
        [FromQuery] bool randomOrder = false)
    {
        var query = new GetAllCategoriesWithProductTypesQuery(
            IncludeImages: includeImages,
            RandomOrder: randomOrder);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("{categorySlug}/types")]
    public async Task<IActionResult> GetCategoryTypes(
        [FromServices] GetCategoryTypesHandler handler,
        string categorySlug,
        [FromQuery] bool includeImages = true,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
    {
        var query = new GetCategoryTypesQuery(
            CategorySlug: categorySlug,
            IncludeImages: includeImages, 
            PageNumber: pageNumber, 
            PageSize: pageSize);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
