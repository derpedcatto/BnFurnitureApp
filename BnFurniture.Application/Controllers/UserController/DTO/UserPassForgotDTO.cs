using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BnFurniture.Application.Controllers.UserController.DTO;

public class UserPassForgotDTO
{
    [JsonPropertyName("emailOrPhone")]
    public string EmailOrPhone { get; set; } = null!;
}

public class PassForgotDTOValidator : AbstractValidator<UserPassForgotDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public PassForgotDTOValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.EmailOrPhone)
            .NotEmpty().WithMessage("Логін не може бути порожнім.")
            .MustAsync(IsLoginValid).WithMessage("Валідація логіну не пройшла перевірку.");
    }

    private async Task<bool> IsLoginValid(string emailOrPhone, CancellationToken ct)
    {
        var user = await GetUser(emailOrPhone, ct);
        return user != null;
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