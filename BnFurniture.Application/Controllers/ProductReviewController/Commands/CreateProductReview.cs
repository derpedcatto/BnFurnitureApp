using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductReviewController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.Commands
{
    public sealed record CreateProductReviewCommand(CreateProductReviewDTO Dto);

    public sealed class CreateProductReviewHandler : CommandHandler<CreateProductReviewCommand>
    {
        private readonly CreateProductReviewDTOValidator _validator;

        public CreateProductReviewHandler(IHandlerContext context, CreateProductReviewDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(CreateProductReviewCommand command, CancellationToken cancellationToken)
        {
            var dto = command.Dto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
                {
                    Message = "Validation failed.",
                    Errors = validationResult.ToApiResponseErrors()
                };
            }

            var newProductReview = new ProductReview
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                AuthorId = dto.AuthorId,
                Rating = dto.Rating,
                Text = dto.Text,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };

            await HandlerContext.DbContext.ProductReview.AddAsync(newProductReview, cancellationToken);
            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.Created)
            {
                Message = "ProductReview created successfully."
            };
        }
    }
}
