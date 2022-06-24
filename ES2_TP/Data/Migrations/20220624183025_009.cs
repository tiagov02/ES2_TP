using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_Categoria_categoriaId",
                table: "PropostasTrabalho");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_Cliente_clienteId",
                table: "PropostasTrabalho");

            migrationBuilder.DropIndex(
                name: "IX_PropostasTrabalho_clienteId",
                table: "PropostasTrabalho");

            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "PropostasTrabalho");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "PropostasTrabalho",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "PropostasTrabalho",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "PropostasTrabalho",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_PropostasTrabalho_categoriaId",
                table: "PropostasTrabalho",
                newName: "IX_PropostasTrabalho_CategoriaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaId",
                table: "PropostasTrabalho",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<float>(
                name: "AnosExperiencia",
                table: "PropostasTrabalho",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "PropostasTrabalho",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalHoras",
                table: "PropostasTrabalho",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTrabalho_SkillId",
                table: "PropostasTrabalho",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_Categoria_CategoriaId",
                table: "PropostasTrabalho",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_Skills_SkillId",
                table: "PropostasTrabalho",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_Categoria_CategoriaId",
                table: "PropostasTrabalho");

            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_Skills_SkillId",
                table: "PropostasTrabalho");

            migrationBuilder.DropIndex(
                name: "IX_PropostasTrabalho_SkillId",
                table: "PropostasTrabalho");

            migrationBuilder.DropColumn(
                name: "AnosExperiencia",
                table: "PropostasTrabalho");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "PropostasTrabalho");

            migrationBuilder.DropColumn(
                name: "TotalHoras",
                table: "PropostasTrabalho");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "PropostasTrabalho",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "PropostasTrabalho",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "PropostasTrabalho",
                newName: "categoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_PropostasTrabalho_CategoriaId",
                table: "PropostasTrabalho",
                newName: "IX_PropostasTrabalho_categoriaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "categoriaId",
                table: "PropostasTrabalho",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "clienteId",
                table: "PropostasTrabalho",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTrabalho_clienteId",
                table: "PropostasTrabalho",
                column: "clienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_Categoria_categoriaId",
                table: "PropostasTrabalho",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_Cliente_clienteId",
                table: "PropostasTrabalho",
                column: "clienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
