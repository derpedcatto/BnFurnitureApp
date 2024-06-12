using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.OrderStatusController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.OrderStatusController.Queries
{
    public sealed record GetAllOrderStatusQuery();

    public sealed class GetAllOrderStatusHandler : QueryHandler<GetAllOrderStatusQuery, List<ResponseOrderStatusDTO>>
    {
        public GetAllOrderStatusHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseOrderStatusDTO>>> Handle(GetAllOrderStatusQuery request, CancellationToken cancellationToken)
        {
            var orderStatuses = await HandlerContext.DbContext.OrderStatus
                .OrderBy(os => os.Id)
                .ToListAsync(cancellationToken);

            var responseDTOs = orderStatuses.Select(status => new ResponseOrderStatusDTO
            {
                Id = status.Id,
                Name = status.Name
            }).ToList();

            return new ApiQueryResponse<List<ResponseOrderStatusDTO>>(true, (int)HttpStatusCode.OK)
            {
                Data = responseDTOs
            };
        }
    }
}
