using BnFurniture.Application.Controllers.ProductTypeController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductTypeController : Controller
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProductTypes([FromServices] GetAllProductTypesHandler handler)
    {
        var query = new GetAllProductTypesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    // TODO: Get all product types of a category
}
