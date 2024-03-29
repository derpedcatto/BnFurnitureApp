using Mediator;
using BnFurniture.Application.Abstractions;
using Microsoft.Extensions.Logging;
using BnFurniture.Application.ExampleController.DTO;

namespace BnFurniture.Application.ExampleController.Queries
{
    public static class GetEntity
    {
        public sealed record Query(int days) : IRequest<Response>;


        public sealed class Response
        {
            public ExampleEntityDTO ExampleEntity { get; private set; }

            public Response(Domain.Entities.ExampleEntity entity)
            {
                ExampleEntity = entity.EntityToDto();
            }
        }


        public sealed class Handler(IHandlerContext context) : QueryHandler<Query, Response>(context)
        {
            public override ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                // Тут происходит взаимодействие с базой данных DbContext. ...
                // Но в примере используются филлерные данные

                string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

                Domain.Entities.ExampleEntity entity = new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(request.days)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                };

                Logger.LogInformation($"Entity got successfully with date {entity.Date}.");

                return ValueTask.FromResult(new Response(entity));
            }
        }
    }
}
