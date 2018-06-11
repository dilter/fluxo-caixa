using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Repositories;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;
        private readonly CalcularSaldo _calcularSaldo;
        private readonly IConsolidacoes _consolidacoes;
        public QueryController(ILogger<QueryController> logger, IContas contas, ILancamentos lancamentos, CalcularSaldo calcularSaldo, IConsolidacoes consolidacoes)
        {
            _logger = logger;
            _contas = contas;
            _lancamentos = lancamentos;
            _calcularSaldo = calcularSaldo;
            _consolidacoes = consolidacoes;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contas = _contas.GetAll().ToList().Select(x => new { conta = x, saldo = _calcularSaldo.Apply(x) });
                var lancamentos = _lancamentos.GetAll().ToList();
                var pagamentos = _lancamentos.FindAllPagamentos().ToList();
                var recebimentos = _lancamentos.FindAllRecebimentos().ToList();
                var consolidacoes = _consolidacoes.GetAll().ToList();
                return Json(new
                {
                    consolidacoes,
                    contas,
                    lancamentos,
                    pagamentos,
                    recebimentos
                });
            }
            catch(Exception e)
            {
                return StatusCode(500, new
                {
                    error = e,
                });
            }                        
        }
    }
}