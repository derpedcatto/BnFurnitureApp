using BnFurniture.Application.Controllers.CharacteristicValueController.DTO.Response;

namespace BnFurniture.Application.Controllers.CharacteristicController.DTO.Response;

public class CharacteristicWithValuesDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }
    public List<CharacteristicValueDTO> Values { get; set; } = [];
}
