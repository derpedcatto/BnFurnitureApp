namespace ASP_Work.Data.Entity
{
    public class ProductCharacteristicConfiguration
    {
        public Guid Id { get; set; }
        public Guid Article_Id { get; set; }
        public Guid Characteristic_id { get; set; }
        public Guid Characteristicvalue_Id { get; set; }

        // Navigation property for the related ProductArticles
        public ProductArticle ProductArticles { get; set; } = null!;
    

    }
}
