using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.App.ExampleController.DTO;
using BnFurniture.Domain.Entities;
using BnFurniture.Domain.Responses;
using System.Net;

namespace BnFurniture.Application.Controllers.App.ExampleController.Queries;

public sealed record GetEntityListQuery();

public sealed class GetEntityListResponse
{
    public IList<ExampleEntityResponseDTO> ExampleEntityList { get; private set; }

    public GetEntityListResponse(IList<ExampleEntityResponseDTO> entityList)
    {
        ExampleEntityList = entityList;
    }
}

public sealed class GetEntityListHandler : QueryHandler<GetEntityListQuery, GetEntityListResponse>
{
    public GetEntityListHandler(IHandlerContext context)
        : base(context)
    {

    }

    public override async Task<ApiQueryResponse<GetEntityListResponse>> Handle(GetEntityListQuery request, CancellationToken cancellationToken)
    {
        // Тут происходит взаимодействие с базой данных DbContext. ...
        // Но в примере используются филлерные данные
        // Сделайте вид, что все эти данные берутся из БД :)
        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
        IList<ExampleEntity> entityList = [];
        for (int i = 0; i < 3; i++)
        {
            var entity = new ExampleEntity()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(5))),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            };
            entityList.Add(entity);
        }

        // Инициализация DTO-модели для класса GetEntityListResponse и отправки на фронт
        IList<ExampleEntityResponseDTO> responseDataList = [];
        foreach (var entity in entityList)
        {
            var dto = new ExampleEntityResponseDTO()
            {
                Date = entity.Date,
                TemperatureC = entity.TemperatureC,
                TemperatureF = entity.TemperatureF,
                Summary = entity.Summary
            };
            responseDataList.Add(dto);
        }

        // Формирование полноценной модели для отправки на сервер
        var responseData = new GetEntityListResponse(responseDataList);
        var response = new ApiQueryResponse<GetEntityListResponse>(true, (int)HttpStatusCode.OK)
        {
            Message = "Entity List returned successfully.",
            Data = responseData
        };

        return response;
    }
}