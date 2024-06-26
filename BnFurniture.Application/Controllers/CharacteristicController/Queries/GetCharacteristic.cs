﻿using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.CharacteristicController.DTO.Response;
using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Response;
using BnFurniture.Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Application.Controllers.CharacteristicController.Queries;

public sealed record GetCharacteristicQuery(string Slug);

public sealed class GetCharacteristicResponse
{
    public CharacteristicDTO Characteristic { get; private set; }
    public List<CharacteristicValueDTO> CharacteristicValues { get; private set; }

    public GetCharacteristicResponse(CharacteristicDTO characteristic, List<CharacteristicValueDTO> characteristicValues)
    {
        Characteristic = characteristic;
        CharacteristicValues = characteristicValues;
    }
}

public sealed class GetCharacteristicHandler : QueryHandler<GetCharacteristicQuery, GetCharacteristicResponse>
{
    public GetCharacteristicHandler(
        IHandlerContext context) : base(context) 
    {
        
    }

    public override async Task<ApiQueryResponse<GetCharacteristicResponse>> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
    {
        var characteristic = await HandlerContext.DbContext.Characteristic
            .Include(c => c.CharacteristicValues)
            .Where(c => c.Slug == request.Slug)
            .FirstOrDefaultAsync(cancellationToken);

        if (characteristic == null)
        {
            return new ApiQueryResponse<GetCharacteristicResponse>(false, 404)
            {
                Message = "Characteristic not found.",
                Data = null
            };
        }

        var response = new GetCharacteristicResponse(
             new CharacteristicDTO
             {
                 Id = characteristic.Id,
                 Name = characteristic.Name,
                 Slug = characteristic.Slug,
                 Priority = characteristic.Priority,
             },
            characteristic.CharacteristicValues
                 .Select(cv => new CharacteristicValueDTO
            {
                 Id = cv.Id,
                 CharacteristicId = cv.CharacteristicId,
                 Value = cv.Value,
                 Slug = cv.Slug,
                 Priority = cv.Priority
             }).OrderBy(c => c.Slug).ToList()
        );

        return new ApiQueryResponse<GetCharacteristicResponse>(true, 200) { Data = response };
    }

}

