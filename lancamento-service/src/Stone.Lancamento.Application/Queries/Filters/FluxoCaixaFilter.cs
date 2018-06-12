using Microsoft.AspNetCore.Mvc;

namespace Stone.Lancamento.Application.Queries.Filters
{
    public class FluxoCaixaFilter
    {
        [FromQuery(Name = "mes")]
        public int Mes { get; set; }
        [FromQuery(Name = "ano")]
        public int Ano { get; set; }
    }
}