using Microsoft.AspNetCore.Mvc;
using BnFurniture.Application.Controllers.UserRegisterController.DTO;
using BnFurniture.Application.Controllers.UserRegisterController.Commands;
using ASP_Work.Services.MailSend;
using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Shared.Utilities.Hash;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BnFurnitureApp.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHashService _hashService;
    private readonly IMailServices _mailServices;
    //private readonly PassForgotDTOValidator _validator;


    public UserController(ApplicationDbContext dbContext, IHashService hashService, IMailServices mailServices)
    {
        _dbContext = dbContext;
        _hashService = hashService;
        _mailServices = mailServices;
        //_validator = validator;
    }

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
    public async Task<IActionResult> PassForgot([FromForm] UserPassForgotDTO model)
    {
        //var validationResult = await _validator.ValidateAsync(model);
        //if (!validationResult.IsValid)
        //{
        //    return BadRequest(validationResult.Errors); // Использование BadRequest для невалидных данных
        //}

        var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            return NotFound(new { status = "NO", message = "Пользователь с указанным логином не найден." });
        }

        var newPassword = GeneratePassword();
        user.Password = _hashService.HashString(newPassword);
        await _dbContext.SaveChangesAsync();
        _mailServices.SendMess(newPassword, user.Email);

        return Ok(new { status = "OK", newPassword = newPassword, mail = "Send message" });
    }
    private string GeneratePassword(int length = 12)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+{}[]|\\:;\"',./<>?`~";
        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = rnd.Next(validChars.Length);
            sb.Append(validChars[index]);
        }

        return sb.ToString();
    }

}