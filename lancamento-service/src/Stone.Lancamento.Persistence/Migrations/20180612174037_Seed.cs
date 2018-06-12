using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Stone.Lancamento.Persistence.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var empresaId = Guid.NewGuid();
            var contaBancariaId = Guid.NewGuid();
            migrationBuilder.InsertData("Empresa",
                new[] {"Id", "Cnpj_Value", "RazaoSocial", "CreationTime", "IsDeleted" },
                new object[] {empresaId.ToString("D"), "15.381.215/0001-77", "DILTER PORTO LADISLAU - ME", DateTime.Now, false }
            );
            migrationBuilder.InsertData("ContaBancaria",
                new[] {"Id", "EmpresaId", "Banco", "Numero", "Tipo", "Limite", "TaxaUtilizacaoLimite", "CreationTime", "IsDeleted"},
                new object[] {contaBancariaId.ToString("D"), empresaId.ToString("D"), "3", "13000715-7", "0", "20000", "0.83", DateTime.Now, false }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
