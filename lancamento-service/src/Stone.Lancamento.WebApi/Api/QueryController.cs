using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stone.Lancamento.Domain.Lancamentos.Repositories;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly ILancamentos _lancamentos;
        public QueryController(ILogger<QueryController> logger, ILancamentos lancamentos)
        {
            _logger = logger;
            _lancamentos = lancamentos;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get()
        {
            var lancamentos = _lancamentos.GetAll().ToList();
            return Json(new {});
        }
    }
}