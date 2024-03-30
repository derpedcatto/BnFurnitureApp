namespace BnFurniture.Application.Controllers.ExampleController.DTO
{
    /// <summary>
    /// Пример DTO-модели, которая отправляется
    /// в контроллер.
    /// </summary>
    public class ExampleEntityDTO
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
    }
}
