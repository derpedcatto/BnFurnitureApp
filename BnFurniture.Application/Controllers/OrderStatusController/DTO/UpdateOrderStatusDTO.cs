using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.DTO
{
    public class UpdateOrderStatusDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
    public class UpdateOrderStatusDTOValidator : AbstractValidator<UpdateOrderStatusDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateOrderStatusDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x)
                .MustAsync((dto, ct) => NameIsUnique(dto, dto.Name, ct)).WithMessage("Name must be unique.");

        }

        private async Task<bool> NameIsUnique(UpdateOrderStatusDTO dto, string name, CancellationToken ct)
        {
            return !await _dbContext.OrderStatus.AnyAsync(os => os.Name == name && os.Id != dto.Id, ct);
        }

    }
}
