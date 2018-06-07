using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Application.Commands.Inputs;

namespace Stone.Lancamento.WebApi.Api
{
    public partial class CommandController
    {
        [HttpPost("lancamentos")]
        public async Task<IActionResult> PostLancamento([FromBody] LancamentoInput input)
        {
            if (!ModelState.IsValid)
            {                
                return StatusCode(400, ModelState.AllErrors());
            }
            return Json(new {});
        }        
    }
}