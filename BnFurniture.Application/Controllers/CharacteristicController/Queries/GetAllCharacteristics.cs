using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;
using BnFurniture.Application.Controllers.ProductCharacteristicController.DTO;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicController.Queries;

public sealed record GetAllCharacteristicsQuery();

public sealed class GetAllCharacteristicsResponse
{
    public List<GetCharacteristicResponse> CharacteristicsList { get; set; }

    public GetAllCharacteristicsResponse(List<GetCharacteristicResponse> characteristicsList)
    {
        CharacteristicsList = characteristicsList;
    }
}

public sealed class GetAllCharacteristicsHandler : QueryHandler<GetAllCharacteristicsQuery, GetAllCharacteristicsResponse>
{
    public GetAllCharacteristicsHandler(
        IHandlerContext context) : base(context)
    {
    
    }

    public override async Task<ApiQueryResponse<GetAllCharacteristicsResponse>> Handle(GetAllCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        var characteristics = await HandlerContext.DbContext.Characteristic
            .Include(c => c.CharacteristicValues)
            .OrderBy(c => c.Slug)
            .ToListAsync(cancellationToken);

        var responseList = characteristics.Select(c => new GetCharacteristicResponse(
            new ResponseCharacteristicDTO
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                Priority = c.Priority
            },
            c.CharacteristicValues
                .OrderBy(cv => cv.Slug)
                .Select(cv => new ResponseCharacteristicValueDTO
            {
                Id = cv.Id,
                CharacteristicId = cv.CharacteristicId,
                Value = cv.Value,
                Slug = cv.Slug,
                Priority = cv.Priority
            }).OrderBy(c => c.Slug).ToList()
        )).ToList();

        return new ApiQueryResponse<GetAllCharacteristicsResponse>(true, 200) { Data = new(responseList) };
    }
}