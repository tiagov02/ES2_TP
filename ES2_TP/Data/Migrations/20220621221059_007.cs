using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalheExperiencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    talentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dt_ini = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_DetalheExperiencia_talentoId",
                table: "DetalheExperiencia",
                column: "talentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheExperiencia");
        }
    }
}
