using Microsoft.AspNetCore.Mvc;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Application.Controllers.App.UserController.DTO;
using BnFurniture.Application.Controllers.App.UserController.Commands;

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

    [HttpPost("passforgot")]
    public async Task<IActionResult> PassForgot([FromServices] PassForgotHandler handler,
        [FromForm] UserPassForgotDTO model)
    {
        var command = new PassForgotCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}