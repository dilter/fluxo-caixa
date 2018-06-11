using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Application.Commands;
using Stone.Lancamento.Application.Commands.Inputs;

namespace Stone.Lancamento.WebApi.Api
{
    public partial class CommandController
    {
        [HttpPost("lancamentos")]
        public async Task<IActionResult> PostLancamento([FromBody] LancamentoInput input)
        {
            var criarLancamentoCommand = new ReceberLancamentoCommand(input);
            if (!ModelState.IsValid)
            {                
                return StatusCode(400, new
                {
                    command = criarLancamentoCommand,
                    status = "error",
                    erros = ModelState.AllErrors(),
                });
            }            
            await _commandBus.SendAsync(criarLancamentoCommand);
            return Json(new
            {
                command = criarLancamentoCommand,
                status = "sent",
            });
        }
    }
}