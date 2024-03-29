using BnFurniture.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    public interface IHandlerContext
    {
        IDbContext DbContext { get; }
        ILogger Logger { get; }
    }
}
