using System;

namespace Stone.Lancamento.Application.Commands.Inputs
{
    public class ConsolidacaoInput
    {
        public Guid ContaBancariaId { get; set; }
        public string Data { get; set; }
    }
}