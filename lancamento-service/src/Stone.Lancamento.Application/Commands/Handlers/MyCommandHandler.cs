using System.Threading.Tasks;
using MediatR;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    public class MyCommandHandler : IAsyncRequestHandler<MyCommand>
    {
        public Task Handle(MyCommand message)
        {
            throw new System.NotImplementedException();
        }
    }
}