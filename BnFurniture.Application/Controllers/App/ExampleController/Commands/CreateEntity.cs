using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.App.ExampleController.DTO;
using BnFurniture.Application.Extensions;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BnFurniture.Application.Controllers.App.ExampleController.Commands;

public sealed record CreateEntityCommand(ExampleEntityFormDTO entityForm);

public sealed class CreateEntityHandler : CommandHandler<CreateEntityCommand>
{
    private readonly ExampleEntityFormDTOValidator _validator;

    public CreateEntityHandler(ExampleEntityFormDTOValidator validator,
        IHandlerContext context) : base(context)
    {
        _validator = validator;
    }

    public override async Task<ApiCommandResponse> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        // Тут происходит взаимодействие с базой данных DbContext. ...
        // Сделайте вид, что все эти данные сохраняются в БД :)

        var dto = request.entityForm;
        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ApiCommandResponse(false, (int)HttpStatusCode.UnprocessableEntity)
            {
                Message = "Валідація не пройшла перевірку.",
                Errors = validationResult.ToApiResponseErrors()
            };
        }

        var newEntity = new ExampleEntity()
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            Summary = dto.Summary
        };

        // Пример использования DbContext:
        // await HandlerContext.DbContext.ExampleEntities.AddAsync(newEntity, cancellationToken);

        HandlerContext.Logger.LogInformation($"Entity created successfully: {Environment.NewLine}"
            + $"Date - {newEntity.Date} {Environment.NewLine}"
            + $"C - {newEntity.TemperatureC} {Environment.NewLine}"
            + $"F - {newEntity.TemperatureF} {Environment.NewLine}"
            + $"Summary - {newEntity.Summary}");

        // Пример использования DbContext:
        // await HandlerContext.DbContext.SaveChangesAsync(cancellationToken);

        return new ApiCommandResponse(true, (int)HttpStatusCode.OK)
        {
            Message = $"Модель створена успішно з датою - {newEntity.Date}"
        };
    }
}