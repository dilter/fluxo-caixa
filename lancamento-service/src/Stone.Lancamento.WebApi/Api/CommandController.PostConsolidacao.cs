using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Application.Commands;
using Stone.Lancamento.Application.Commands.Inputs;

namespace Stone.Lancamento.WebApi.Api
{
    public partial class CommandController
    {
        [HttpPost("lancamentos/consolidacao")]
        public async Task<IActionResult> PostConsolidacao([FromBody] ConsolidacaoInput input)
        {
            var consolidarLancamentosCommand = new ConsolidarLancamentosCommand(input);
            if (!ModelState.IsValid)
            {
                return StatusCode(400, new
                {
                    command = consolidarLancamentosCommand,
                    status = "error",
                    erros = ModelState.AllErrors(),
                });
            }

            await _commandBus.SendAsync(consolidarLancamentosCommand);
            return Json(new
            {
                command = consolidarLancamentosCommand,
                status = "sent",
            });
        }
    }
}