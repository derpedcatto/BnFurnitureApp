namespace ASP_Work.Data.Entity
{
    public class ProductSetItem
    {
        public Guid Id { get; set; }
        public Guid ProductSet_Id { get; set; }
        public Guid Product_Id { get; set; }

        // Navigation property for the related Products
        public Product Products { get; set; } = null!;

        // Navigation property for the related ProductSet
        public ProductSet ProductSet { get; set; } = null!;

    }
}
