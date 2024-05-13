using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class Characteristic
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    // Nav
    public ICollection<CharacteristicValue> CharacteristicValues { get; set; } = null!;
    public ICollection<ProductCharacteristicConfiguration> ProductCharacteristicConfigurations { get; set; } = null!;
}
