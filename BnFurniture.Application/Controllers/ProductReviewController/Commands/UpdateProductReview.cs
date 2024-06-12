using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductReviewController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.Commands
{
    public sealed record UpdateProductReviewCommand(UpdateProductReviewDTO Dto);

    public sealed class UpdateProductReviewHandler : CommandHandler<UpdateProductReviewCommand>
    {
        private readonly UpdateProductReviewDTOValidator _validator;

        public UpdateProductReviewHandler(UpdateProductReviewDTOValidator validator, IHandlerContext context) : base(context)
        {
            _validator = validator;
        }

        public override async Task<ApiCommandResponse> Handle(UpdateProductReviewCommand command, CancellationToken cancellationToken)
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

            var review = await HandlerContext.DbContext.ProductReview
                .FirstOrDefaultAsync(r => r.Id == dto.Id, cancellationToken);

            if (review == null)
            {
                return new ApiCommandResponse(false, (int)HttpStatusCode.NotFound)
                {
                    Message = "ProductReview not found."
                };
            }

            review.Rating = dto.Rating;
            review.Text = dto.Text;
            review.UpdatedAt = dto.UpdatedAt;

            await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

            return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
            {
                Message = "ProductReview updated successfully."
            };
        }
    }
}
