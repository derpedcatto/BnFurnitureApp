using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderController.Queries
{
    public sealed record GetAllOrdersQuery();

    public sealed class GetAllOrdersHandler : QueryHandler<GetAllOrdersQuery, List<ResponseOrderDTO>>
    {
        public GetAllOrdersHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseOrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await HandlerContext.DbContext.Order
                .Select(o => new ResponseOrderDTO
                {
                  
                    UserId = o.UserId,
                    StatusId = o.StatusId,
                    CreatedAt = o.CreatedAt,
                    ModifiedAt = o.ModifiedAt
                })
                .ToListAsync(cancellationToken);

            return new ApiQueryResponse<List<ResponseOrderDTO>>(true, 200) { Data = orders };
        }
    }
}
