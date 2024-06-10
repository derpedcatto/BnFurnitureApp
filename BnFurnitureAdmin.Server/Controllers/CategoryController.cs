using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.CategoryController.Commands;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Controllers.CategoryController.Queries;

namespace BnFurnitureAdmin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
// [AuthorizePermissions(Permissions.DashboardAccess)]
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

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromServices] CreateCategoryHandler handler,
        [FromForm] CreateCategoryDTO model)
    {
        var command = new CreateCategoryCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromServices] UpdateCategoryHandler handler,
        [FromBody] UpdateCategoryDTO model)
    {
        var command = new UpdateCategoryCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut("image")]
    public async Task<IActionResult> SetCategoryImage([FromServices] SetCategoryImageHandler handler,
        [FromForm] SetCategoryImageDTO model)
    {
        var command = new SetCategoryImageCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory([FromServices] DeleteCategoryHandler handler,
        Guid categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
