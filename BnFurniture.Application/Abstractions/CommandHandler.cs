using BnFurniture.Domain.Responses;

namespace BnFurniture.Application.Abstractions;

public abstract class CommandHandler<TCommand>
{
    protected readonly IHandlerContext HandlerContext;

    protected CommandHandler(IHandlerContext context)
    {
        HandlerContext = context;
    }

    public abstract Task<ApiCommandResponse> Handle(TCommand request, CancellationToken cancellationToken = default);
}