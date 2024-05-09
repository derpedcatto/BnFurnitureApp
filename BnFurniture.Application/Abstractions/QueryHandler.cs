using BnFurniture.Domain.Responses;

namespace BnFurniture.Application.Abstractions;

public abstract class QueryHandler<TQuery, TResponse>
{
    protected readonly IHandlerContext HandlerContext;

    protected QueryHandler(IHandlerContext context)
    {
        HandlerContext = context;
    }

    public abstract Task<ApiQueryResponse<TResponse>> Handle(TQuery request, CancellationToken cancellationToken = default);
}
