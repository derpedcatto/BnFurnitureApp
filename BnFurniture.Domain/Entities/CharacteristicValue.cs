using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class CharacteristicValue
{
    [Key]
    public Guid Id { get; set; }
    public Guid CharacteristicId { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    // Nav
    [ForeignKey(nameof(CharacteristicId))]
    public Characteristic Characteristic { get; set; } = null!;
    public ICollection<ProductCharacteristicConfiguration> ProductCharacteristicConfigurations { get; set; } = null!;
}
