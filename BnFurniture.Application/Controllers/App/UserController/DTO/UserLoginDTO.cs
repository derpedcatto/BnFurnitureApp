using BnFurniture.Infrastructure.Persistence;
using BnFurniture.Shared.Utilities.Hash;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.App.UserController.DTO;

public class UserLoginDTO
{
    [FromForm(Name = "emailOrPhone")]
    public string EmailOrPhone { get; set; } = null!;

    [FromForm(Name = "password")]
    public string Password { get; set; } = null!;
}

public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHashService _hashService;

    public UserLoginDTOValidator(ApplicationDbContext dbContext, IHashService hashService)
    {
        _dbContext = dbContext;
        _hashService = hashService;

        RuleFor(x => x.EmailOrPhone)
            .NotEmpty().WithMessage("Логін не може бути порожнім.")
            .MustAsync(IsLoginValid).WithMessage("Валідація логіну не пройшла перевірку.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не може бути порожнім.")
            .MustAsync((dto, password, ct) => { return IsPasswordValid(password, dto.EmailOrPhone, ct); })
                .WithMessage("Валідація паролю не пройшла перевірку.");
    }

    private async Task<bool> IsLoginValid(string emailOrPhone, CancellationToken ct)
    {
        var user = await GetUser(emailOrPhone, ct);
        return user != null;
    }

    private async Task<bool> IsPasswordValid(string password, string emailOrPhone, CancellationToken ct)
    {
        var user = await GetUser(emailOrPhone, ct);

        if (user == null) { return false; }
        return user.Password == _hashService.HashString(password);
    }

    private async Task<Domain.Entities.User?> GetUser(string emailOrPhone, CancellationToken ct)
    {
        if (IsEmail(emailOrPhone))
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Email == emailOrPhone, ct);
        }
        else
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.PhoneNumber == emailOrPhone, ct);
        }
    }

    private bool IsEmail(string emailOrPhone)
    {
        return emailOrPhone.Contains('@');
    }
}