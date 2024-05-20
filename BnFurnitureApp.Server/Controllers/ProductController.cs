using BnFurniture.Application.Controllers.CategoryController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureAdmin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories([FromServices] GetAllCategoriesHandler handler)
    {
        var query = new GetAllCategoriesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
