using BnFurniture.Application.Controllers.CharacteristicValueController.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BnFurnitureApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCharacteristicValueController : ControllerBase
{
    [HttpGet("{CharacteristicSlug}")]
    public async Task<IActionResult> GetAllCharacteristicValues([FromServices] GetAllCharacteristicValuesHandler handler,
        string CharacteristicSlug)
    {
        var query = new GetAllCharacteristicValuesQuery(CharacteristicSlug);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}
