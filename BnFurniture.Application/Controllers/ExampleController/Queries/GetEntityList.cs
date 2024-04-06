using BnFurniture.Application.Abstractions;
using BnFurniture.Application.Controllers.ExampleController.DTO;
using Mediator;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Controllers.ExampleController.Queries
{
    /// <summary>
    /// Пример запроса по чтению коллекции Entity из БД и отправки данных в контроллер.
    /// </summary>
    public static class GetEntityList
    {
        /// <summary>
        /// Описание запроса и его параметров.
        /// </summary>
        public sealed record Query() : IRequest<Response>;

        /// <summary>
        /// Возвращаемый ответ.
        /// </summary>
        public sealed class Response
        {
            public IList<ExampleEntityDTO> ExampleEntityList { get; private set; }

            public Response(IList<ExampleEntityDTO> entityList)
            {
                ExampleEntityList = entityList;
            }
        }

        /// <summary>
        /// Класс-обработчик запроса.
        /// </summary>
        public sealed class Handler : QueryHandler<Query, Response>
        {
            /// <summary>
            /// Конструктор.
            /// </summary>
            /// <param name="context"></param>
            public Handler(IHandlerContext context)
                : base(context)
            {

            }

            /// <summary>
            /// Функция-обработчик запроса.
            /// </summary>
            /// <param name="request">Переданные параметры запроса (<see cref="Response"/>)</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
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
