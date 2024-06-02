using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.Queries
{
    public sealed record GetAllCharacteristicValuesQuery();

    public sealed class GetAllCharacteristicValuesHandler : QueryHandler<GetAllCharacteristicValuesQuery, List<ResponseCharacteristicValueDTO>>
    {
        public GetAllCharacteristicValuesHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<List<ResponseCharacteristicValueDTO>>> Handle(GetAllCharacteristicValuesQuery request, CancellationToken cancellationToken)
        {
            var characteristicValues = await HandlerContext.DbContext.CharacteristicValue
                .Select(cv => new ResponseCharacteristicValueDTO
                {
                    Id = cv.Id,
                    CharacteristicId = cv.CharacteristicId,
                    Value = cv.Value,
                    Slug = cv.Slug,
                    Priority = cv.Priority
                })
                .ToListAsync(cancellationToken);

            return new ApiQueryResponse<List<ResponseCharacteristicValueDTO>>(true, 200) { Data = characteristicValues };
        }
    }
}
