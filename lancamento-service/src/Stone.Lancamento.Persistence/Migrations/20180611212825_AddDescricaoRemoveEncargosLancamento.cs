using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Stone.Lancamento.Persistence.Migrations
{
    public partial class AddDescricaoRemoveEncargosLancamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encargos",
                table: "Lancamento");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Recebimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Pagamento",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Recebimento");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Pagamento");

            migrationBuilder.AddColumn<decimal>(
                name: "Encargos",
                table: "Lancamento",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
