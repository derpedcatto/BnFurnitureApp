namespace ASP_Work.Data.Entity
{
    public class ProductSetCategory
    {
        public Guid Id { get; set; }
        public Guid Parent_id { get; set; }
        public String Name { get; set; } = null!;
        public int? Priority { get; set; }

        // Navigation property for the related ProductSet
        public ICollection<ProductSet>? ProductSets { get; set; } = null!;

    }
}
