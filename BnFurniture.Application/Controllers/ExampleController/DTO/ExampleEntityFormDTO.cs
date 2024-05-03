using FluentValidation;

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
    /*
    public class ExampleEntityFormDTOValidator : AbstractValidator<ExampleEntityFormDTO>
    {
        public ExampleEntityFormDTOValidator() 
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.TemperatureC)
                .LessThan(250)
                .GreaterThan(-250);
        }
    }
    */
}
