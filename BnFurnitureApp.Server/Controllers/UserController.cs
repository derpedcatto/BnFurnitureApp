using Microsoft.AspNetCore.Mvc;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Application.Controllers.UserController.Commands;
using BnFurniture.Application.Controllers.UserController.DTO;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromServices] SignUpHandler handler,
        [FromBody] UserSignUpDTO model)
    {
        var command = new SignUpCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] LoginHandler handler,
        [FromBody] UserLoginDTO model)
    {
        var command = new LoginCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpPost("passforgot")]
    public async Task<IActionResult> PassForgot([FromServices] PassForgotHandler handler,
        [FromBody] UserPassForgotDTO model)
    {
        var command = new PassForgotCommand(model);

        var apiResponse = await handler.Handle(command, CancellationToken.None);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}