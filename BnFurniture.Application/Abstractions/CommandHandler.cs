using BnFurniture.Infrastructure.Persistence;
using Mediator;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : IRequest<TResult>
    {
        protected readonly ILogger Logger;
        protected readonly IDbContext DbContext;

        protected CommandHandler(IHandlerContext context)
        {
            Logger = context.Logger;
            DbContext = context.DbContext;
        }

        public abstract ValueTask<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    }
}
