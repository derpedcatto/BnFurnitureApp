using BnFurniture.Application.Controllers.ProductController.Commands;
using BnFurniture.Application.Controllers.ProductController.DTO;
using BnFurniture.Application.Controllers.ProductController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureAdmin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
// [AuthorizePermissions(Permissions.DashboardAccess)]
public class ProductController : Controller
{
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct([FromServices] GetProductHandler handler,
        Guid productId)
    {
        var query = new GetProductQuery(productId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromServices] CreateProductHandler handler,
        [FromBody] CreateProductDTO model)
    {
        var command = new CreateProductCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromServices] UpdateProductHandler handler,
        [FromBody] UpdateProductDTO model)
    {
        var command = new UpdateProductCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct([FromServices] DeleteProductHandler handler,
        Guid productId)
    {
        var command = new DeleteProductCommand(productId);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
