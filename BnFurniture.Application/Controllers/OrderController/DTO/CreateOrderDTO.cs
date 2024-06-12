using System;
using System.Text.Json.Serialization;
using FluentValidation;
using BnFurniture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.Order.DTO
{
    public class CreateOrderDTO
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("statusId")]
        public int StatusId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }
    }

    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty.");
            RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("StatusId must be greater than 0.");
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("CreatedAt cannot be empty.");
        }
    }
}
