using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class create1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Information1",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information2",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information3",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information4",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information5",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information6",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information7",
                table: "Informations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information8",
                table: "Informations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information1",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information2",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information3",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "Information4",
                table: "Informations");

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
    }
}
