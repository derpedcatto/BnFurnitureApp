using BnFurniture.Application.Abstractions;
using Mediator;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BnFurniture.Application.ExampleController.DTO;

namespace BnFurniture.Application.ExampleController.Commands
{
    public static class CreateEntity
    {
        public sealed record Command(ExampleEntityFormDTO entityForm) : IRequest<Response>;


        public sealed class Response
        {

        }


        public sealed class Handler : CommandHandler<Command, Response>
        {
            public Handler(IHandlerContext context) 
                : base(context)
            {

            }

            public override async ValueTask<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                // Interaction with the database DbContext happens here.
                // For demonstration purposes, we're using placeholder data.

                Domain.Entities.ExampleEntity newEntity = request.entityForm.FormDtoToEntity();

                // Пример использования DbContext:
                // await DbContext.ExampleEntities.AddAsync(entity, cancellationToken);
                // await DbContext.SaveChangesAsync(cancellationToken);

                Logger.LogInformation($"Entity created successfully:"
                    + $"\nDate - {newEntity.Date}\n"
                    + $"C - {newEntity.TemperatureC}\n"
                    + $"F - {newEntity.TemperatureF}\n"
                    + $"Summary - {newEntity.Summary}");

                return new Response();
            }
        }
    }
}