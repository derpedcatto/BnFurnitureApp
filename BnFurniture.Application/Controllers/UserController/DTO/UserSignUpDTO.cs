using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using BnFurniture.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.UserController.DTO;

public class UserSignUpDTO
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("repeatPassword")]
    public string RepeatPassword { get; set; } = null!; // TODO

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }    // TODO

    [JsonPropertyName("agreeCheckbox")]
    public bool Agree { get; set; }  // TODO
}

public class UserSignUpDTOValidator : AbstractValidator<UserSignUpDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public UserSignUpDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Електронна пошта не може бути порожнью.")
            .MustAsync(IsLoginUsed).WithMessage("Електронна пошта вже зареєстрована.")
            .EmailAddress().WithMessage("Електронна пошта не є валідною.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не може бути порожнім.")
            .MinimumLength(5).WithMessage("Пароль повинен містити щонайменше 5 символів.")
            .Equal(x => x.RepeatPassword).WithMessage("Паролі повинні співпадати.");

        RuleFor(x => x.RepeatPassword)
            .NotEmpty().WithMessage("Повторний пароль не може бути порожнім.")
            .Equal(x => x.Password).WithMessage("Паролі повинні співпадати.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ім'я не може бути порожнім.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Прізвище не може бути порожнім.");

        RuleFor(x => x.MobileNumber)
            .PhoneNumber().WithMessage("Номер телефона не є валідним. Приклад валідного номеру = '380980473401'").When(x => x.MobileNumber != null);

        RuleFor(x => x.Address)
            .NotEmpty().When(x => x.Address != null);

        RuleFor(x => x.Agree)
            .Equal(true).WithMessage("Погодьтесь з Політикою Конфіденційності.");

        // TODO: Address validation?
    }

    private async Task<bool> IsLoginUsed(string login, CancellationToken cancellationToken)
    {
        var result = await _dbContext.User.AnyAsync(u => u.Email == login, cancellationToken);
        return !result;
    }
}

/* Strong password check
    .Must(p =>
     p.Any(char.IsUpper) && p.Any(char.IsLower) && p.Any(char.IsDigit) && p.Any(IsSpecialCharacter))
     .WithMessage("Пароль должен содержать как минимум один знак верхнего регистра, один знак нижнего регистра, одну цифру и один специальный символ.")

    private bool IsSpecialCharacter(char c)
    {
        return !char.IsLetterOrDigit(c);
    }
*/