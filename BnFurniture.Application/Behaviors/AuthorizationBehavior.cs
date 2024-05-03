using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BnFurniture.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public ValueTask<TResponse> Handle(
            TRequest message, 
            CancellationToken cancellationToken,
            MessageHandlerDelegate<TRequest, TResponse> next)
        {
            throw new NotImplementedException();
        }
    }
}
