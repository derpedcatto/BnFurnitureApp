using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.UserRegisterController.DTO;
using BnFurniture.Application.Controllers.UserRegisterController.Commands;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromServices] SignUpHandler handler,
        [FromForm] UserSignUpDTO model)
    {
        var command = new SignUpCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] LoginHandler handler,
        [FromForm] UserLoginDTO model)
    {
        var command = new LoginCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}