using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistController.DTO
{
    public class CreateUserWishlistDTO
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
    }

    public class CreateUserWishlistDTOValidator : AbstractValidator<CreateUserWishlistDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateUserWishlistDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MustAsync(UserExists).WithMessage("User with given ID does not exist.");
        }

        private async Task<bool> UserExists(Guid userId, CancellationToken ct)
        {
            return await _dbContext.User.AnyAsync(u => u.Id == userId, ct);
        }
    }
}
