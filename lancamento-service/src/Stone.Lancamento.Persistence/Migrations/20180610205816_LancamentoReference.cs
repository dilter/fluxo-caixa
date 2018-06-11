using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Stone.Lancamento.Persistence.Migrations
{
    public partial class LancamentoReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LancamentoId",
                table: "Recebimento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LancamentoId",
                table: "Pagamento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_LancamentoId",
                table: "Recebimento",
                column: "LancamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_LancamentoId",
                table: "Pagamento",
                column: "LancamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_Lancamento_LancamentoId",
                table: "Pagamento",
                column: "LancamentoId",
                principalTable: "Lancamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recebimento_Lancamento_LancamentoId",
                table: "Recebimento",
                column: "LancamentoId",
                principalTable: "Lancamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_Lancamento_LancamentoId",
                table: "Pagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Recebimento_Lancamento_LancamentoId",
                table: "Recebimento");

            migrationBuilder.DropIndex(
                name: "IX_Recebimento_LancamentoId",
                table: "Recebimento");

            migrationBuilder.DropIndex(
                name: "IX_Pagamento_LancamentoId",
                table: "Pagamento");

            migrationBuilder.DropColumn(
                name: "LancamentoId",
                table: "Recebimento");

            migrationBuilder.DropColumn(
                name: "LancamentoId",
                table: "Pagamento");
        }
    }
}
