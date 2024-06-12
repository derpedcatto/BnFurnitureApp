using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ProductReviewController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.ProductReviewController.Queries
{
    public sealed record GetAllProductReviewsQuery();

    public sealed class GetAllProductReviewsHandler : QueryHandler<GetAllProductReviewsQuery, List<ResponseProductReviewDTO>>
    {
        public GetAllProductReviewsHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseProductReviewDTO>>> Handle(GetAllProductReviewsQuery request, CancellationToken ct)
        {
            var productReviews = await HandlerContext.DbContext.ProductReview
                .Include(pr => pr.Product)
                .Include(pr => pr.Author)
                .OrderBy(pr => pr.CreatedAt)
                .ToListAsync(ct);

            var responseDTOs = productReviews.Select(pr => new ResponseProductReviewDTO
            {
                Id = pr.Id,
                ProductId = pr.ProductId,
                AuthorId = pr.AuthorId,
                Rating = pr.Rating,
                Text = pr.Text,
                CreatedAt = pr.CreatedAt,
                UpdatedAt = pr.UpdatedAt
            }).ToList();

            return new ApiQueryResponse<List<ResponseProductReviewDTO>>(true, (int)HttpStatusCode.OK)
            {
                Data = responseDTOs
            };
        }
    }
}
