using System;

namespace BnFurniture.Application.Controllers.CharacteristicController.DTO.Response;

public class CharacteristicDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }
}

