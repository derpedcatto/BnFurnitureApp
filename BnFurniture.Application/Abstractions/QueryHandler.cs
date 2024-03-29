using BnFurniture.Infrastructure.Persistence;
using Mediator;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IRequest<TResponse>
    {
        protected readonly ILogger Logger;
        protected readonly IDbContext DbContext;

        protected QueryHandler(IHandlerContext context)
        {
            Logger = context.Logger;
            DbContext = context.DbContext;
        }

        public abstract ValueTask<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
