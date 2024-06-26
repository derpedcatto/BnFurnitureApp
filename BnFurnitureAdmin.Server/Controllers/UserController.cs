﻿using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.UserController.Commands;
using BnFurniture.Application.Controllers.UserController.Queries;
using BnFurniture.Domain.Responses;
using System.Net;
using System.Security.Claims;
using BnFurniture.Application.Controllers.UserController.DTO.Request;

namespace BnFurnitureAdmin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> Login([FromServices] UserLoginHandler handler,
        [FromBody] UserLoginDTO model)
    {
        var command = new UserLoginCommand(model);

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

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromServices] GetUserByIdHandler handler,
    Guid userId)
    {
        var query = new GetUserByIdQuery(userId);

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser(
        [FromServices] GetCurrentUserHandler handler)
    {
        var query = new GetCurrentUserQuery();

        var apiResponse = await handler.Handle(query, HttpContext.RequestAborted);
        return new JsonResult(apiResponse) { StatusCode = apiResponse.StatusCode };
    }
}