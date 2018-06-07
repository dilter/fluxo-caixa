using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        public QueryController(ILogger<QueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get()
        {
            return Json(new {});
        }
    }
}