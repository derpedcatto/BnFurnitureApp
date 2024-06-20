using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicValueController.Queries;

public sealed record GetAllCharacteristicValuesQuery(string CharacteristicSlug);

public sealed class GetAllCharacteristicValuesResponse
{
    public List<CharacteristicValueDTO> CharacteristicValuesList { get; set; }

    public GetAllCharacteristicValuesResponse(List<CharacteristicValueDTO> characteristicValuesList)
    {
        CharacteristicValuesList = characteristicValuesList;
    }
}

public sealed class GetAllCharacteristicValuesHandler : QueryHandler<GetAllCharacteristicValuesQuery, GetAllCharacteristicValuesResponse>
{
    public GetAllCharacteristicValuesHandler(
        IHandlerContext context) : base(context)
    {
        
    }

    public override async Task<ApiQueryResponse<GetAllCharacteristicValuesResponse>> Handle(GetAllCharacteristicValuesQuery request, CancellationToken cancellationToken)
    {
        var characteristic = await HandlerContext.DbContext.Characteristic
            .Include(c => c.CharacteristicValues)
            .Where(c => c.Slug == request.CharacteristicSlug)
            .FirstOrDefaultAsync(cancellationToken);

        if (characteristic == null)
        {
            return new ApiQueryResponse<GetAllCharacteristicValuesResponse>(false, 404)
            {
                Message = "Characteristic not found.",
                Data = null
            };
        }

        var characteristicValues = characteristic.CharacteristicValues
            .Select(cv => new CharacteristicValueDTO
            {
                Id = cv.Id,
                CharacteristicId = cv.CharacteristicId,
                Value = cv.Value,
                Slug = cv.Slug,
                Priority = cv.Priority
            })
            .OrderBy(cv => cv.Slug)
            .ToList();

        return new ApiQueryResponse<GetAllCharacteristicValuesResponse>(true, 200) { Data = new(characteristicValues) };
    }
}
