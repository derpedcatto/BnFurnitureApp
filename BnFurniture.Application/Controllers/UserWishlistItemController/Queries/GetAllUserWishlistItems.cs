using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.UserWishlistItemController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.UserWishlistItemController.Queries
{
    public sealed record GetAllUserWishlistItemsQuery();

    public sealed class GetAllUserWishlistItemsHandler : QueryHandler<GetAllUserWishlistItemsQuery, List<ResponseUserWishlistItemDTO>>
    {
        public GetAllUserWishlistItemsHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseUserWishlistItemDTO>>> Handle(GetAllUserWishlistItemsQuery request, CancellationToken cancellationToken)
        {
            var userWishlistItems = await HandlerContext.DbContext.UserWishlistItem
                .Include(uwi => uwi.ProductArticle)
                .Include(uwi => uwi.UserWishlist)
                .OrderBy(uwi => uwi.AddedAt)
                .ToListAsync(cancellationToken);

            var responseDTOs = userWishlistItems.Select(item => new ResponseUserWishlistItemDTO
            {
                Id = item.Id,
                ProductArticleId = item.ProductArticleId,
                UserWishlistId = item.UserWishlistId,
                AddedAt = item.AddedAt
            }).ToList();

            return new ApiQueryResponse<List<ResponseUserWishlistItemDTO>>(true, (int)HttpStatusCode.OK)
            {
                Data = responseDTOs
            };
        }
    }
}
