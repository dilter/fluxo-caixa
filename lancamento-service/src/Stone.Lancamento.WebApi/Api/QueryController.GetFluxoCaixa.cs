using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Application.Queries;
using Stone.Lancamento.Application.Queries.Filters;

namespace Stone.Lancamento.WebApi.Api
{
    public partial class QueryController
    {
        [HttpGet("fluxo-caixa")]
        public async Task<IActionResult> GetFluxoCaixa([FromQuery] FluxoCaixaFilter filter)
        {
            try
            {
                var queryFluxoDeCaixa = new GetFluxoDeCaixaQuery(filter);
                var output = await _mediator.Send(queryFluxoDeCaixa);
                return Json(output);

                //                var contas = _contas.GetAll().ToList().Select(x => new {conta = x, saldo = _calcularSaldo.Apply(x)});
//                var lancamentos = _lancamentos.GetAll().ToList();
//                var pagamentos = _lancamentos.FindAllPagamentos().ToList();
//                var recebimentos = _lancamentos.FindAllRecebimentos().ToList();
//                var consolidacoes = _consolidacoes.GetAll().ToList();
//                return Json(new
//                {
//                    consolidacoes,
//                    contas,
//                    lancamentos,
//                    pagamentos,
//                    recebimentos
//                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    error = e,
                });
            }
        }
    }
}