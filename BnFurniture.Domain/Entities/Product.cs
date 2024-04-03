namespace BnFurniture.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        //public Guid Article { get; set; }
        public Guid Productype_Id { get; set; }
        public Guid Author_Id { get; set; }
        public String Name { get; set; } = null!;
        public String? Summary { get; set; } 
        public String? Description { get; set; }
        public String? Productdetails { get; set; }
        public int? Priority { get; set; }
        public Boolean Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } 

        // Navigation property for the related ProductType
        public ProductType ProductType   { get; set; } = null!;

        // Navigation property for the related Products
        public ICollection<Product> Products { get; set; } = null!;

        // Navigation property for the related ProductSetItem
        public ICollection <ProductSetItem> ProductSetItem { get; set; } = null!;

        // Navigation property for the related ProductReview
        public ICollection <ProductReview> ProductReview { get; set; } = null!;

        // Navigation property for the related ProductReview
        public ICollection<ProductArticle> ProductArticles { get; set; } = null!;

        // Navigation property for the related ProductMetrics
        public ProductMetrics Metrics { get; set; } = null!;

        // Navigation property for the related User
        public User User_Pr { get; set; } = null!;
    }
}
