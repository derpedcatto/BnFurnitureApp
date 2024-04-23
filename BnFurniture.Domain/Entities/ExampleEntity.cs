namespace BnFurniture.Domain.Entities
{
    public class ExampleEntity
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => TemperatureC * 2;
        public string? Summary { get; set; }
    }
}
