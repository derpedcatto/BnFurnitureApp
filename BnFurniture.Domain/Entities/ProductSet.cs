    namespace ASP_Work.Data.Entity
{
    public class ProductSet
    {
        public Guid Id { get; set; }
        public Guid Setcategory_Id { get; set; }
        public Guid Author_Id { get; set; }
        public String Name { get; set; } = null!;
        public String? Summary { get; set; } 
        public String? Description { get; set; } 
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int? Priority { get; set; } = null;

        // Navigation property for the related ProductSetItem
        public ICollection<ProductSetItem>?ProductSetItems { get; set; }= null!;

        // Navigation property for the related ProductSetCategory
        public ProductSetCategory? ProductSetCategory { get; set; }=null!;

        // Navigation property for the related ProductSetCategory
        public User? User_Ps { get; set; } = null!;
    }
}
