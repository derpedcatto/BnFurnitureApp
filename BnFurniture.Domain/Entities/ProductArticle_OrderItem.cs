namespace BnFurniture.Domain.Entities;

public class ProductArticle_OrderItem
{
    public Guid ProductArticleId { get; set; }
    public Guid? OrderItemId { get; set; }

    // Nav
    public ProductArticle ProductArticle { get; set; } = null!;
    public OrderItem? OrderItem { get; set; } = null!;
}
