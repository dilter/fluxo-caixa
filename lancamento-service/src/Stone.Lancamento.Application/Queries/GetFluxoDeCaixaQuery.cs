using System.Collections.Generic;
using MediatR;
using Stone.Lancamento.Application.Queries.Filters;
using Stone.Lancamento.Application.Queries.Outputs;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Queries
{
    public class GetFluxoDeCaixaQuery : IQuery<IEnumerable<FluxoCaixaOutput>>
    {
        public FluxoCaixaFilter Filter { get; }
        public GetFluxoDeCaixaQuery(FluxoCaixaFilter filter)
        {
            this.Filter = filter;
        }
    }
}