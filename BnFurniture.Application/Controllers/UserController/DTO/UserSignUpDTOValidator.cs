using FluentValidation;

namespace BnFurniture.Application.Controllers.UserController.DTO;

public class UserSignUpDTOValidator : AbstractValidator<UserSignUpDTO>
{
    // private readonly ApplicationDbContext _dbContext;

    // public UserSignUpDTOValidator(ApplicationDbContext dbContext)
    public UserSignUpDTOValidator()
    {
        // _dbContext = dbContext;

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логин не может быть пустым")
            .Must(IsLoginNotUsed).WithMessage("Введёный логин уже зарегестрирован")
            .EmailAddress().WithMessage("Электронная почта не валидна");

        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Пароль должен иметь как минимум 6 знаков")
            .Must(p =>
                p.Any(char.IsUpper) && p.Any(char.IsLower) && p.Any(char.IsDigit) && p.Any(IsSpecialCharacter))
                .WithMessage("Пароль должен содержать как минимум один знак верхнего регистра, один знак нижнего регистра, одну цифру и один специальный символ")
            .Equal(x => x.RepeatPassword).WithMessage("Пароли должны быть одинаковыми");

        RuleFor(x => x.RepeatPassword)
            .NotEmpty().WithMessage("Пароль должен быть записан повторно");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия не может быть пустой");

        RuleFor(x => x.Agree)
            .Equal(true).WithMessage("Надо согласиться с условиями");

        // TODO: Phone number validation
        // TODO: address validation?
    }

    private bool IsLoginNotUsed(string login)
    {
        return true;
        // return ! _dbContext.User.Any(u => u.Email == login);
    }

    private bool IsSpecialCharacter(char c)
    {
        return !char.IsLetterOrDigit(c);
    }
}
