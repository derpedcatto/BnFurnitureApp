using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CategoryController.DTO;
using BnFurniture.Application.Controllers.CategoryController.Queries;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Controllers.CharacteristicController.Queries
{
   
    public sealed record GetCharacteristicQuery(string Slug);

    public sealed class GetCharacteristicResponse
    {
        public ResponseCharacteristicDTO Characteristic { get; private set; }
        public List<CharacteristicValueDTO> CharacteristicValues { get; private set; }

        public GetCharacteristicResponse(ResponseCharacteristicDTO characteristic, List<CharacteristicValueDTO> characteristicValues)
        {
            Characteristic = characteristic;
            CharacteristicValues = characteristicValues;
        }
    }

    public sealed class GetCharacteristicHandler : QueryHandler<GetCharacteristicQuery, GetCharacteristicResponse>
    {
        public GetCharacteristicHandler(IHandlerContext context) : base(context) { }

        public override async Task<ApiQueryResponse<GetCharacteristicResponse>> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            var characteristic = await HandlerContext.DbContext.Characteristic
                .Include(c => c.CharacteristicValues)
                .Where(c => c.Slug == request.Slug)//сортировку по Slug для Characteristics
                .OrderBy(c => c.Slug) //

                .FirstOrDefaultAsync(cancellationToken);

            if (characteristic == null)
            {
                return new ApiQueryResponse<GetCharacteristicResponse>(false, 404)
                {
                    Message = "Characteristic not found."
                };
            }

            var response = new GetCharacteristicResponse(
                new ResponseCharacteristicDTO
                {
                    Id = characteristic.Id,
                    Name = characteristic.Name,
                    Slug = characteristic.Slug,
                    Priority = characteristic.Priority,
                },
               //сортировку по Value для CharacteristicValues.
               characteristic.CharacteristicValues.OrderBy(cv => cv.Value)
               .Select(cv => new CharacteristicValueDTO
               {
                    Id = cv.Id,
                    CharacteristicId = cv.CharacteristicId,
                    Value = cv.Value,
                    Slug = cv.Slug,
                    Priority = cv.Priority
                }).ToList()
            );

            return new ApiQueryResponse<GetCharacteristicResponse>(true, 200) { Data = response };
        }

    }
   
}
