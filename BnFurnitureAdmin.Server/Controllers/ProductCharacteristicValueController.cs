using BnFurniture.Application.Controllers.CharacteristicValueController.Commands;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Request;
using BnFurniture.Application.Controllers.CharacteristicValueController.Queries;
using BnFurniture.Application.Controllers.ProductCharacteristicController.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureAdmin.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
// [AuthorizePermissions(Permissions.DashboardAccess)]
public class ProductCharacteristicValueController : ControllerBase
{
    [HttpGet("{CharacteristicSlug}")]
    public async Task<IActionResult> GetAllCharacteristicValues(
        [FromServices] GetAllCharacteristicValuesHandler handler,
        string CharacteristicSlug)
    {
        var query = new GetAllCharacteristicValuesQuery(CharacteristicSlug);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacteristicValue(
        [FromServices] CreateCharacteristicValueHandler handler,
        [FromBody] CreateCharacteristicValueDTO dto)
    {
        var command = new CreateCharacteristicValueCommand(dto);

        var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacteristicValue(
        [FromServices] UpdateCharacteristicValueHandler handler, 
        [FromBody] UpdateCharacteristicValueDTO dto)
    {
        var command = new UpdateCharacteristicValueCommand(dto);

        var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacteristicValue(
        [FromServices] DeleteCharacteristicValueHandler handler,
        Guid id)
    {
        var command = new DeleteCharacteristicValueCommand(id);

        var apiResponse = await handler.Handle(command, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
