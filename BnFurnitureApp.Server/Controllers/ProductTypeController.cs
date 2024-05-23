﻿using BnFurniture.Application.Controllers.ProductTypeController.Commands;
using BnFurniture.Application.Controllers.ProductTypeController.DTO;
using BnFurniture.Application.Controllers.ProductTypeController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
// [AuthorizePermissions(Permissions.DashboardAccess)]
public class ProductTypeController : Controller
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllProductTypes([FromServices] GetAllProductTypesHandler handler)
    {
        var query = new GetAllProductTypesQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductType([FromServices] CreateProductTypeHandler handler,
        [FromBody] CreateProductTypeDTO model)
    {
        var command = new CreateProductTypeCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductType([FromServices] UpdateProductTypeHandler handler,
        [FromBody] UpdateProductTypeDTO model)
    {
        var command = new UpdateProductTypeCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{productTypeId:guid}")]
    public async Task<IActionResult> DeleteProductType([FromServices] DeleteProductTypeHandler handler,
        Guid productTypeId)
    {
        var command = new DeleteProductTypeCommand(productTypeId);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
