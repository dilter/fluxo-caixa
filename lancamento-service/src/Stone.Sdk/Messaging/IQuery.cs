using MediatR;

namespace Stone.Sdk.Messaging
{
    public interface IQuery<out TOut> : IRequest<TOut> {}
}