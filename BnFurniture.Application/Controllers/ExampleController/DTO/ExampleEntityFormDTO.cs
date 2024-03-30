namespace BnFurniture.Application.Controllers.ExampleController.DTO
{
    /// <summary>
    /// Пример DTO-модели, которая принимается
    /// из Формы с Frontend.
    /// </summary>
    public class ExampleEntityFormDTO
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
