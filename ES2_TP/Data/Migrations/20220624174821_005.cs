using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdSkill",
                table: "Talento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "Talento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talento_SkillId",
                table: "Talento",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Talento_Skills_SkillId",
                table: "Talento",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talento_Skills_SkillId",
                table: "Talento");

            migrationBuilder.DropIndex(
                name: "IX_Talento_SkillId",
                table: "Talento");

            migrationBuilder.DropColumn(
                name: "IdSkill",
                table: "Talento");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "Talento");
        }
    }
}
