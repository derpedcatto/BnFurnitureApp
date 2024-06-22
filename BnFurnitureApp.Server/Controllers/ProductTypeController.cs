using BnFurniture.Application.Controllers.ProductTypeController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductTypeController : Controller
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProductTypes(
        [FromServices] GetAllProductTypesHandler handler,
        [FromQuery] bool includeImages = true,
        [FromQuery] bool randomOrder = false,
        [FromQuery] int? pageSize = null,
        [FromQuery] int? pageNumber = null)
    {
        var query = new GetAllProductTypesQuery(
            RandomOrder: randomOrder,
            IncludeImages: includeImages,
            PageSize: pageSize,
            PageNumber: pageNumber);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
