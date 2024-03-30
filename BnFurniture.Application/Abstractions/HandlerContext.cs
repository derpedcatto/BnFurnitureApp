using BnFurniture.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    public class HandlerContext(ApplicationDbContext dbContext, ILogger<HandlerContext> logger) : IHandlerContext
    {
        public ApplicationDbContext DbContext { get; private set; } = dbContext;
        public ILogger Logger { get; private set;} = logger;
    }
}
