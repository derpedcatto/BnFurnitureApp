namespace BnFurniture.Domain.Entities
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public Guid Category_id { get; set; }
        public String Name { get; set; } = null!;
        public int? Priority { get; set; } = null;

        // Navigation property for the related Products
        public ICollection<Product> Products { get; set; } = null!;

        // Navigation property for the related ProductCategory
        public ProductCategory ProductCategory { get; set; } = null!;
        // Navigation property for the related ProductTypeTop
        //public ProductTypeTop ProductTypeTop { get; set; } =null!; TABLE DELETED
        // Foreign key to ProductTypeTop
        public Guid ProducType_Id { get; set; }

    }
}
