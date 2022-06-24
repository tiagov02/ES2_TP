using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TalentoId",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "idTalento",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_TalentoId",
                table: "Cliente",
                column: "TalentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Talento_TalentoId",
                table: "Cliente",
                column: "TalentoId",
                principalTable: "Talento",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Talento_TalentoId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_TalentoId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "TalentoId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "idTalento",
                table: "Cliente");
        }
    }
}
