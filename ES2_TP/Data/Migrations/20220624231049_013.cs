using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES2_TP.Data.Migrations
{
    public partial class _013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "Talento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "Talento");
        }
    }
}
