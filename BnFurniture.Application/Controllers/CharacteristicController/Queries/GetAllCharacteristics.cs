using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicController.Queries
{
    public sealed class GetAllCharacteristicsQuery { }

    public sealed class GetAllCharacteristicsHandler : QueryHandler<GetAllCharacteristicsQuery, List<GetCharacteristicResponse>>
    {
        public GetAllCharacteristicsHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<GetCharacteristicResponse>>> Handle(GetAllCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            var characteristics = await HandlerContext.DbContext.Characteristic
                .Include(c => c.CharacteristicValues)
                .OrderBy(c => c.Slug)
                .ToListAsync(cancellationToken);

            var response = characteristics.Select(c => new GetCharacteristicResponse(
                new ResponseCharacteristicDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    Priority = c.Priority
                },
                c.CharacteristicValues.OrderBy(cv => cv.Value).Select(cv => new CharacteristicValueDTO
                {
                    Id = cv.Id,
                    CharacteristicId = cv.CharacteristicId,
                    Value = cv.Value,
                    Slug = cv.Slug,
                    Priority = cv.Priority
                }).ToList()
            )).ToList();

            return new ApiQueryResponse<List<GetCharacteristicResponse>>(true, 200) { Data = response };
        }
    }
}
