using BnFurniture.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductArticle_OrderItem.DTO
{
    public class CreateProductArticle_OrderItemDTO
    {
        public Guid ProductArticleId { get; set; }
        public Guid? OrderItemId { get; set; }
    }

    public class CreateProductArticle_OrderItemDTOValidator : AbstractValidator<CreateProductArticle_OrderItemDTO>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateProductArticle_OrderItemDTOValidator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.ProductArticleId)
                .NotEmpty().WithMessage("ProductArticleId is required.")
                .MustAsync(ProductArticleExists).WithMessage("ProductArticle with given ID does not exist.");

            RuleFor(x => x.OrderItemId)
                .MustAsync(OrderItemExists).When(x => x.OrderItemId.HasValue).WithMessage("OrderItem with given ID does not exist.");
        }

        private async Task<bool> ProductArticleExists(Guid productArticleId, CancellationToken cancellationToken)
        {
            return await _dbContext.ProductArticle.AnyAsync(pa => pa.Article == productArticleId, cancellationToken);
        }

        private async Task<bool> OrderItemExists(Guid? orderItemId, CancellationToken cancellationToken)
        {
            if (!orderItemId.HasValue) return true;
            return await _dbContext.OrderItem.AnyAsync(oi => oi.Id == orderItemId.Value, cancellationToken);
        }
    }
}
