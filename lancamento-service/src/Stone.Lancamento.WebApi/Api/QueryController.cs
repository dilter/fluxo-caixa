using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Stone.Lancamento.WebApi.Api
{
    [Route("api")]
    public partial class QueryController : Controller
    {        
        private readonly ILogger<QueryController> _logger;
        private readonly IMediator _mediator;
        public QueryController(ILogger<QueryController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}