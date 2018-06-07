using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stone.Lancamento.Application.Commands.Inputs;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class CommandController : Controller
    {
        private readonly ILogger<CommandController> _logger;
        public CommandController(ILogger<CommandController> logger)
        {
            _logger = logger;
        }
    }
}
