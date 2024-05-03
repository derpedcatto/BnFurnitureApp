using Mediator;
using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.UserController.DTO;
using BnFurniture.Application.Controllers.UserController.Commands;
using System.ComponentModel.DataAnnotations;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserRegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm] UserSignUpDTO? model)
    {
        try
        {
            var query = new SignUp.Command(model);
            var response = await _mediator.Send(query);

            return Ok(response);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}