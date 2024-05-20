using BnFurnitureAdmin.Server.Attributes;
using BnFurniture.Domain.Constants;
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
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromServices] CreateCategoryHandler handler,
        [FromBody] CreateCategoryDTO model)
    {
        var command = new CreateCategoryCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategories([FromServices] GetAllCategoriesHandler handler)
    {
        var query = new GetAllCategoriesQuery();

        var apiResponse = await handler.Handle(query, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
