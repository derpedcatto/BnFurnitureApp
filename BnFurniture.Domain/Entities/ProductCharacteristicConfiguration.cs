using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductCharacteristicConfiguration
{
    [Key]
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid CharacteristicId { get; set; }
    public Guid CharacteristicValueId { get; set; }

    // Nav
    [ForeignKey(nameof(ArticleId))]
    public ProductArticle ProductArticle { get; set; } = null!;

    [ForeignKey(nameof(CharacteristicId))]
    public Characteristic Characteristic { get; set; } = null!;

    [ForeignKey(nameof(CharacteristicValueId))]
    public CharacteristicValue CharacteristicValue { get; set; } = null!;
}
