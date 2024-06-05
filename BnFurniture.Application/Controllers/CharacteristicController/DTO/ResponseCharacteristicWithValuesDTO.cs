using BnFurniture.Application.Controllers.CharacteristicValueController.DTO;

namespace BnFurniture.Application.Controllers.ProductController.DTO;

public class ResponseCharacteristicWithValuesDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }
    public List<ResponseCharacteristicValueDTO> Values { get; set; } = [];
}
