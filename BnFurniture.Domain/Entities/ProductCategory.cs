namespace BnFurniture.Domain.Entities
{
    public class ProductCategory
    {
            public Guid Id { get; set; }
            public Guid? Parent_id { get; set; }
            public String Name { get; set; } = null!;
            public int? Priority { get; set; } = null;

        // Navigation property for the related ProductTypes
        public ICollection<ProductType> ProductType { get; set; } = null!;

        // Navigation property for the related ProductCategoryTop
        public Guid Category_Id { get; set; }  // Add this property
        //public ProductCategoryTop ProductCategoryTop { get; set; } = null!; // Navigation property


    }
}
