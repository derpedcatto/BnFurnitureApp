using BnFurniture.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BnFurniture.Application.Abstractions
{
    /// <summary>
    /// Общий контекст всех обработчиков запроса.
    /// </summary>
    public interface IHandlerContext
    {
        ApplicationDbContext DbContext { get; }
        ILogger Logger { get; }
    }
}
