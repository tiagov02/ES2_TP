using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Talento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    precoHora = table.Column<float>(type: "real", nullable: false),
                    horasExperiencia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropostasTalento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    propostaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tempoEstimado = table.Column<float>(type: "real", nullable: true),
                    valor = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostasTalento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropostasTalento_PropostasTrabalho_propostaId",
                        column: x => x.propostaId,
                        principalTable: "PropostasTrabalho",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropostasTalento_Talento_talentoId",
                        column: x => x.talentoId,
                        principalTable: "Talento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTalento_propostaId",
                table: "PropostasTalento",
                column: "propostaId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTalento_talentoId",
                table: "PropostasTalento",
                column: "talentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropostasTalento");

            migrationBuilder.DropTable(
                name: "Talento");
        }
    }
}
