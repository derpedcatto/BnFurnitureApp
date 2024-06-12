using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.DTO
{
    public class CreateOrderStatusDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        //[Required(ErrorMessage = "Name is required.")]
        //[StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; } = string.Empty;
    }

    public class CreateOrderStatusDTOValidator : AbstractValidator<CreateOrderStatusDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateOrderStatusDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name length must be between 1 and 100 characters.")
                .MustAsync(NameIsUnique).WithMessage("Name must be unique.");
        }

        private async Task<bool> NameIsUnique(string name, CancellationToken ct)
        {
            return !await _dbContext.OrderStatus.AnyAsync(os => os.Name == name, ct);
        }
    }
}
