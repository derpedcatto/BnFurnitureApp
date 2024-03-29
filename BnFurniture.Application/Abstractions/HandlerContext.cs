using BnFurniture.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    public class HandlerContext(IDbContext dbContext, ILogger<HandlerContext> logger) : IHandlerContext
    {
        public IDbContext DbContext { get; private set; } = dbContext;
        public ILogger Logger { get; private set;} = logger;
    }
}
