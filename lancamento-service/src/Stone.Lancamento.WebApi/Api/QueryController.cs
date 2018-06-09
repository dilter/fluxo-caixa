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
        private readonly IEmpresas _empresas;

        public QueryController(ILogger<QueryController> logger, IEmpresas empresas)
        {
            _logger = logger;
            _empresas = empresas;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get()
        {            
            var empresas = _empresas.GetAll().ToList();            
            return Json(new {});
        }
    }
}