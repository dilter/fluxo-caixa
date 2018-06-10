using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;
        public QueryController(ILogger<QueryController> logger, IContas contas, ILancamentos lancamentos)
        {
            _logger = logger;
            _contas = contas;
            _lancamentos = lancamentos;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contas = _contas.GetAll().ToList();
                var lancamentos = _lancamentos.GetAll().ToList();
                var pagamentos = _lancamentos.GetAllPagamentos().ToList();
                var recebimentos = _lancamentos.GetAllRecebimentos().ToList();
                return Json(new
                {
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