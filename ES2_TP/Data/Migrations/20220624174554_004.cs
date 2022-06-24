using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_AspNetUsers_clienteId",
                table: "PropostasTrabalho");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Categoria_categoriaId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "categoriaId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "idCategoria",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "clienteId",
                table: "PropostasTrabalho",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_Cliente_clienteId",
                table: "PropostasTrabalho",
                column: "clienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Categoria_categoriaId",
                table: "Skills",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropostasTrabalho_Cliente_clienteId",
                table: "PropostasTrabalho");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Categoria_categoriaId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropColumn(
                name: "idCategoria",
                table: "Skills");

            migrationBuilder.AlterColumn<Guid>(
                name: "categoriaId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "clienteId",
                table: "PropostasTrabalho",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PropostasTrabalho_AspNetUsers_clienteId",
                table: "PropostasTrabalho",
                column: "clienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Categoria_categoriaId",
                table: "Skills",
                column: "categoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
