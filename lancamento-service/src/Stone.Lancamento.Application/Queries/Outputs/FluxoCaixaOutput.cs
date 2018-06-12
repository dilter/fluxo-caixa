using System.Collections.Generic;

namespace Stone.Lancamento.Application.Queries.Outputs
{
    public class FluxoCaixaOutput
    {
        public string Data { get; set; }
        public IEnumerable<EntradaOutput> Entradas { get; set; }
        public IEnumerable<SaidaOutput> Saidas { get; set; }
        public IEnumerable<EncargoOutput> Encargos { get; set; }
        public string Total { get; set; }
        public string PosicaoDoDia { get; set; }
    }
}