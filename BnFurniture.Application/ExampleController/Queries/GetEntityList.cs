using BnFurniture.Application.Abstractions;
using BnFurniture.Application.ExampleController.DTO;
using Mediator;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.ExampleController.Queries
{
    public static class GetEntityList
    {
        public sealed record Query() : IRequest<Response>;

        public sealed class Response
        {
            public IList<ExampleEntityDTO> ExampleEntityList { get; private set; }

            public Response(IList<ExampleEntityDTO> entityList)
            {
                ExampleEntityList = entityList;
            }
        }

        public sealed class Handler : QueryHandler<Query, Response>
        {
            public Handler(IHandlerContext context) 
                : base(context)
            {

            }

            public override ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                // Тут происходит взаимодействие с базой данных DbContext. ...
                // Но в примере используются филлерные данные

                string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

                IList<Domain.Entities.ExampleEntity> entityList =
                [
                    new()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(5))),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    },
                    new()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(5))),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    },
                    new()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(5))),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    },
                ];

                IList<ExampleEntityDTO> responseList = [];
                foreach (var entity in entityList)
                {
                    responseList.Add(entity.EntityToDto());
                }

                Logger.LogInformation($"Entity list got successfully fetched.");

                return ValueTask.FromResult(new Response(responseList));
            }
        }
    }
}
