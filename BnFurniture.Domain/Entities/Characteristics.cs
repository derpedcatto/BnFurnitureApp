namespace ASP_Work.Data.Entity
{
    public class Characteristics
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public int? Priority { get; set; } = null;

        // Navigation property for the related CharacteristicsValue
        public ICollection<CharacteristicsValue> CharacteristicsValues { get; set; } = null!;

    }
}
