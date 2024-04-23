using BnFurniture.Application.Abstractions;
using Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BnFurniture.Application.Controllers.ExampleController.DTO;

namespace BnFurniture.Application.Controllers.ExampleController.Commands
{
    /// <summary>
    /// Пример комманды по созданию нового Entity в БД.
    /// </summary>
    public static class CreateEntity
    {
        /// <summary>
        /// Описание комманды и её параметров.
        /// </summary>
        /// <param name="entityForm">Данные, которые надо обработать и сохранить в БД.</param>
        public sealed record Command(ExampleEntityFormDTO entityForm) : IRequest<Response>;

        /// <summary>
        /// Возвращаемый ответ.
        /// </summary>
        public sealed class Response
        {

        }

        /// <summary>
        /// Класс-обработчик комманды.
        /// </summary>
        public sealed class Handler : CommandHandler<Command, Response>
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
            /// Функция-обработчик комманды.
            /// </summary>
            /// <param name="request">Переданные параметры запроса (<see cref="Command"/>)</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public override async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                // Тут происходит взаимодействие с базой данных DbContext. ...
                // Но в примере используются филлерные данные

                // Пример использования DbContext:
                // await DbContext.ExampleEntities.AddAsync(entity, cancellationToken);
                Domain.Entities.ExampleEntity newEntity = request.entityForm.FormDtoToEntity();

                Logger.LogInformation($"Entity created successfully:"
                    + $"\nDate - {newEntity.Date}\n"
                    + $"C - {newEntity.TemperatureC}\n"
                    + $"F - {newEntity.TemperatureF}\n"
                    + $"Summary - {newEntity.Summary}");

                // await DbContext.SaveChangesAsync(cancellationToken);
                return new Response();
            }
        }
    }
}