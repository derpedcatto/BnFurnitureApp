using BnFurniture.Application.Controllers.ExampleController.Commands;
using BnFurniture.Application.Controllers.ExampleController.DTO;
using BnFurniture.Application.Controllers.ExampleController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureAdmin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetExampleEntity(
        [FromServices] GetEntityHandler handler,
        int days)
    {
        var query = new GetEntityQuery(days);
         
        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetExampleEntityList(
        [FromServices] GetEntityListHandler handler)
    {
        var query = new GetEntityListQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost]
    public async Task<IActionResult> CreateExampleEntity(
        [FromServices] CreateEntityHandler handler,
        [FromBody] ExampleEntityFormDTO model)
    {
        var command = new CreateEntityCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}