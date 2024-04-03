namespace BnFurniture.Domain.Entities
{
    public class ProductMetrics
    {
        public Guid Id { get; set; }
        public Guid Product_Id { get; set; }
        public long? Views { get; set; } = 0;
        public long? Sales { get; set; } = 0;

        // Navigation property for the related ProductMetrics
        public Product? Product { get; set; } = null!;
    }
}
