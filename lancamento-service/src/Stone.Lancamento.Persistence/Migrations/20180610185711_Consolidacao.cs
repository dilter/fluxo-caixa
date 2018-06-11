using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Stone.Lancamento.Persistence.Migrations
{
    public partial class Consolidacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConsolidacaoId",
                table: "Recebimento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConsolidacaoId",
                table: "Pagamento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consolidacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consolidacao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_ConsolidacaoId",
                table: "Recebimento",
                column: "ConsolidacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_ConsolidacaoId",
                table: "Pagamento",
                column: "ConsolidacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_Consolidacao_ConsolidacaoId",
                table: "Pagamento",
                column: "ConsolidacaoId",
                principalTable: "Consolidacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recebimento_Consolidacao_ConsolidacaoId",
                table: "Recebimento",
                column: "ConsolidacaoId",
                principalTable: "Consolidacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_Consolidacao_ConsolidacaoId",
                table: "Pagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Recebimento_Consolidacao_ConsolidacaoId",
                table: "Recebimento");

            migrationBuilder.DropTable(
                name: "Consolidacao");

            migrationBuilder.DropIndex(
                name: "IX_Recebimento_ConsolidacaoId",
                table: "Recebimento");

            migrationBuilder.DropIndex(
                name: "IX_Pagamento_ConsolidacaoId",
                table: "Pagamento");

            migrationBuilder.DropColumn(
                name: "ConsolidacaoId",
                table: "Recebimento");

            migrationBuilder.DropColumn(
                name: "ConsolidacaoId",
                table: "Pagamento");
        }
    }
}
