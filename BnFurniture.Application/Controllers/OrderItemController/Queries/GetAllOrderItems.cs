using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderItem.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderItem.Queries
{
    public sealed record GetAllOrderItemsQuery();
    public sealed class GetAllOrderItemsHandler : QueryHandler<GetAllOrderItemsQuery, List<ResponseOrderItemDTO>>
    {
        public GetAllOrderItemsHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseOrderItemDTO>>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await HandlerContext.DbContext.OrderItem
                .Include(oi => oi.ProductArticle)
                .OrderBy(oi => oi.OrderId)
                .ToListAsync(cancellationToken);

            var responseDTOs = orderItems.Select(item => new ResponseOrderItemDTO
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ArticleId = item.ArticleId,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            }).ToList();

            return new ApiQueryResponse<List<ResponseOrderItemDTO>>(true, (int)HttpStatusCode.OK)
            {
                Data = responseDTOs
            };
        }
    }
}
