using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillsTalento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    skillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    preco = table.Column<float>(type: "real", nullable: true),
                    numHoras = table.Column<float>(type: "real", nullable: false)
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
                name: "IX_SkillsTalento_skillId",
                table: "SkillsTalento",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsTalento_talentoId",
                table: "SkillsTalento",
                column: "talentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillsTalento");
        }
    }
}
