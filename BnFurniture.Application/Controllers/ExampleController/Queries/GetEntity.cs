//using Mediator;
//using BnFurniture.Application.Abstractions;
//using Microsoft.Extensions.Logging;
//using BnFurniture.Application.Controllers.ExampleController.DTO;

//namespace BnFurniture.Application.Controllers.ExampleController.Queries
//{
//    /// <summary>
//    /// Пример запроса по чтению Entity из БД и отправки данных в контроллер.
//    /// </summary>
//    public static class GetEntity
//    {
//        /// <summary>
//        /// Описание запроса и его параметров.
//        /// </summary>
//        /// <param name="days">Кол-во дней, через сколько надо просчитать прогноз погоды.</param>
//        public sealed record Query(int days) : IRequest<Response>;

//        /// <summary>
//        /// Возвращаемый ответ.
//        /// </summary>
//        public sealed class Response
//        {
//            public ExampleEntityDTO ExampleEntity { get; private set; }

//            public Response(ExampleEntityDTO entity)
//            {
//                ExampleEntity = entity;
//            }
//        }

//        /// <summary>
//        /// Класс-обработчик запроса.
//        /// </summary>
//        public sealed class Handler : QueryHandler<Query, Response>
//        {
//            /// <summary>
//            /// Конструктор.
//            /// </summary>
//            /// <param name="context"></param>
//            public Handler(IHandlerContext context)
//                : base(context)
//            {

//            }

//            /// <summary>
//            /// Функция-обработчик запроса.
//            /// </summary>
//            /// <param name="request">Переданные параметры запроса (<see cref="Response"/>)</param>
//            /// <param name="cancellationToken"></param>
//            /// <returns></returns>
//            public override ValueTask<Response> Handle(Query request, CancellationToken cancellationToken)
//            {
//                // Тут происходит взаимодействие с базой данных DbContext. ...
//                // Но в примере используются филлерные данные

//                string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

//                Domain.Entities.ExampleEntity entity = new()
//                {
//                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(request.days)),
//                    TemperatureC = Random.Shared.Next(-20, 55),
//                    Summary = summaries[Random.Shared.Next(summaries.Length)]
//                };

//                Logger.LogInformation($"Entity got successfully with date {entity.Date}.");

//                return ValueTask.FromResult( new Response( ExampleEntityMapper.EntityToDto(entity) ) );
//            }
//        }
//    }
//}
