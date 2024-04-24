using Microsoft.AspNetCore.Mvc;

namespace BnFurniture.Application.Controllers.UserRegisterController.DTO;

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
