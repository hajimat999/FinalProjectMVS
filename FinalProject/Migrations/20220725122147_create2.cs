using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information5",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information6",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information7",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information8",
                table: "Informations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Information5",
                table: "Informations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information6",
                table: "Informations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information7",
                table: "Informations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information8",
                table: "Informations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
