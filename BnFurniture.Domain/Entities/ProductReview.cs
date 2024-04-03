namespace BnFurniture.Domain.Entities
{
    public class ProductReview
    {
        public Guid Id { get; set; }
        public Guid Product_id { get; set; }
        public Guid User_Id { get; set; }
        public int Rating { get; set; }
        public String Text { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        // Navigation property for the related Products
        public Product Products { get; set; } = null!;

        // Navigation property for the related Products
        public User User_Pr { get; set; } = null!;
    }
}
