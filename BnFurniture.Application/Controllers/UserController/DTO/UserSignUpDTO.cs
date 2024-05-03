using BnFurniture.Infrastructure.Persistence;
using FluentValidation;

namespace BnFurniture.Application.Controllers.UserController.DTO;

public class UserSignUpDTO
{
    public String Login { get; set; } = null!;
    public String Password { get; set; } = null!;
    public String RepeatPassword { get; set; } = null!;
    public String Name { get; set; } = null!;
    public String LastName { get; set; } = null!;
    public String? Phone { get; set; }
    public String? Address { get; set; }
    public Boolean Agree { get; set; }
}




/*
using Microsoft.AspNetCore.Mvc;

public class UserSignUpDTO
{
    [FromForm(Name = "user-login")]
    public String Login { get; set; } = null!;

    [FromForm(Name = "user-pass")]
    public String Password { get; set; } = null!;

    [FromForm(Name = "user-passrepeat")]
    public String RepeatPassword { get; set; } = null!;

    [FromForm(Name = "user-firstname")]
    public String Name { get; set; } = null!;

    [FromForm(Name = "user-lastname")]
    public String LastName { get; set; } = null!;

    [FromForm(Name = "user-phone")]
    public String? Phone { get; set; }

    [FromForm(Name = "user-address")]
    public String? Address { get; set; }

    [FromForm(Name = "user-confirm")]
    public Boolean Agree { get; set; }
}
*/