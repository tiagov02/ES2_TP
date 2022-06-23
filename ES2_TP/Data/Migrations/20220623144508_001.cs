using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropostasTrabalho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clienteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    categoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostasTrabalho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropostasTrabalho_AspNetUsers_clienteId",
                        column: x => x.clienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropostasTrabalho_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    precoHora = table.Column<float>(type: "real", nullable: false),
                    horasExperiencia = table.Column<float>(type: "real", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talento_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_PropostasTrabalho_categoriaId",
                table: "PropostasTrabalho",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PropostasTrabalho_clienteId",
                table: "PropostasTrabalho",
                column: "clienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_categoriaId",
                table: "Skills",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsTalento_skillId",
                table: "SkillsTalento",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsTalento_talentoId",
                table: "SkillsTalento",
                column: "talentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Talento_CategoriaId",
                table: "Talento",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheExperiencia");

            migrationBuilder.DropTable(
                name: "PropostasTalento");

            migrationBuilder.DropTable(
                name: "SkillsTalento");

            migrationBuilder.DropTable(
                name: "PropostasTrabalho");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Talento");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
