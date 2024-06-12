using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderItem.DTO
{
    public class CreateOrderItemDTO
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("articleId")]
        public Guid ArticleId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discount")]
        public int Discount { get; set; }
    }

    public class CreateOrderItemDTOValidator : AbstractValidator<CreateOrderItemDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateOrderItemDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.")
                .MustAsync(OrderExists).WithMessage("Order with given ID does not exist.");

            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage("ArticleId is required.")
                .MustAsync(ArticleExists).WithMessage("Article with given ID does not exist.");

            RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");


            // У нас была уже такая проверка ,не знаю нужно ли ее здесь повторять???

            //RuleFor(x => x.Discount)
            //    .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");
        }

        //private async Task<bool> OrderExists(Guid order_id, CancellationToken ct)
        //{
        //    return await _dbContext.Order.AnyAsync(o => o.Id == order_id, ct);
        //}

        private async Task<bool> OrderExists(Guid orderId, CancellationToken ct)
        {
            Console.WriteLine($"Checking existence of Order with ID: {orderId}");
            var exists = await _dbContext.Order.AnyAsync(o => o.Id == orderId, ct);
            Console.WriteLine($"Order exists: {exists}");
            return exists;
        }

        private async Task<bool> ArticleExists(Guid articleId, CancellationToken ct)
        {
            return await _dbContext.ProductArticle.AnyAsync(a => a.Article == articleId, ct);
        }
    }

}
