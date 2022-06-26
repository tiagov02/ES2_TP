using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheExperiencia");

            migrationBuilder.DropTable(
                name: "PropostasTalento");

            migrationBuilder.DropTable(
                name: "SkillsTalento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalheExperiencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_ini = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheExperiencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalheExperiencia_Talento_talentoId",
                        column: x => x.talentoId,
                        principalTable: "Talento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropostasTalento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    propostaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "SkillsTalento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    skillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    numHoras = table.Column<float>(type: "real", nullable: false),
                    preco = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsTalento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillsTalento_Skills_skillId",
                        column: x => x.skillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SkillsTalento_Talento_talentoId",
                        column: x => x.talentoId,
                        principalTable: "Talento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalheExperiencia_talentoId",
                table: "DetalheExperiencia",
                column: "talentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTalento_propostaId",
                table: "PropostasTalento",
                column: "propostaId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTalento_talentoId",
                table: "PropostasTalento",
                column: "talentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsTalento_skillId",
                table: "SkillsTalento",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsTalento_talentoId",
                table: "SkillsTalento",
                column: "talentoId");
        }
    }
}
