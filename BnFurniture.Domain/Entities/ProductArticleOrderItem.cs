namespace BnFurniture.Domain.Entities
{
    public class ProductArticleOrderItem
    {
        public Guid ProductArticleId { get; set; }
        public ProductArticle ProductArticle { get; set; } = null!;

        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; } = null!;
    }
}
