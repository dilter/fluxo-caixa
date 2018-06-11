using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class CommandController : Controller
    {
        private readonly ILogger<CommandController> _logger;
        private readonly ICommandBus _commandBus;
        public CommandController(ILogger<CommandController> logger, ICommandBus commandBus)
        {
            _logger = logger;
            _commandBus = commandBus;
        }
    }
}
