using BnFurniture.Application.Abstractions;
using BnFurniture.Domain.Responses;
using BnFurniture.Domain.Entities;
using System.Net;
using BnFurniture.Application.Controllers.App.ExampleController.DTO;

namespace BnFurniture.Application.Controllers.App.ExampleController.Queries;

public sealed record GetEntityQuery(int days);

public sealed class GetEntityResponse
{
    public ExampleEntityResponseDTO ExampleEntity { get; private set; }

    public GetEntityResponse(ExampleEntityResponseDTO entity)
    {
        ExampleEntity = entity;
    }
}

public sealed class GetEntityHandler : QueryHandler<GetEntityQuery, GetEntityResponse>
{
    public GetEntityHandler(IHandlerContext context)
        : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetEntityResponse>> Handle(GetEntityQuery request, CancellationToken cancellationToken)
    {
        // Тут происходит взаимодействие с базой данных DbContext. ...
        // Но в примере используются филлерные данные
        // Сделайте вид, что все эти данные берутся из БД :)

        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
        ExampleEntity entity = new()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(request.days)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        };

        // Инициализация DTO-модели для класса GetEntityResponse и отправки на фронт
        var dto = new ExampleEntityResponseDTO()
        {
            Date = entity.Date,
            TemperatureC = entity.TemperatureC,
            TemperatureF = entity.TemperatureF,
            Summary = entity.Summary
        };

        // Формирование полноценной модели для отправки на сервер
        var responseData = new GetEntityResponse(dto);
        var response = new ApiQueryResponse<GetEntityResponse>(true, (int)HttpStatusCode.OK)
        {
            Message = $"Entity got successfully returned with date {entity.Date}.",
            Data = responseData
        };

        return response;
    }
}
