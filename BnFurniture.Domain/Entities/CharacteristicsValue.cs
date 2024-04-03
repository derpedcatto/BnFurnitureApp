namespace ASP_Work.Data.Entity
{
    public class CharacteristicsValue
    {
        public Guid Id { get; set; }
        public Guid Сharacteristic_id { get; set; }
        public String Value { get; set; } = null!;
        public int? Priority { get; set; } = null;

        // Navigation property for the related Characteristics
        public Characteristics Characteristics { get; set; } = null!;
    }
}
