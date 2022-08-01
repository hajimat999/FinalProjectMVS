using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class create5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Clothes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Color_Id",
                table: "Clothes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_ColorId",
                table: "Clothes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Color_ColorId",
                table: "Clothes",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Color_ColorId",
                table: "Clothes");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_ColorId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "Color_Id",
                table: "Clothes");
        }
    }
}
