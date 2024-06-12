using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.Order.DTO
{
    public class UpdateOrderDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("statusId")]
        public int StatusId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateOrderDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .MustAsync(IsIdValid).WithMessage("Order with this Id does not exist.");

            RuleFor(x => x.UserId)
                .MustAsync(IsUserIdValid).WithMessage("User with this Id does not exist.");

            RuleFor(x => x.StatusId)
                .MustAsync(IsStatusIdValid).WithMessage("Status with this Id does not exist.");
        }

        private async Task<bool> IsIdValid(Guid id, CancellationToken ct)
        {
            return await _dbContext.Order.AnyAsync(o => o.Id == id, ct);
        }

        private async Task<bool> IsUserIdValid(Guid userId, CancellationToken ct)
        {
            return await _dbContext.User.AnyAsync(u => u.Id == userId, ct);
        }

        private async Task<bool> IsStatusIdValid(int statusId, CancellationToken ct)
        {
            return await _dbContext.OrderStatus.AnyAsync(s => s.Id == statusId, ct);
        }
    }
}