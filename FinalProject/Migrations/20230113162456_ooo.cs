using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class ooo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Clothes_ClothingId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "ClothingId",
                table: "BasketItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Clothes_ClothingId",
                table: "BasketItems",
                column: "ClothingId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Clothes_ClothingId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "ClothingId",
                table: "BasketItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Clothes_ClothingId",
                table: "BasketItems",
                column: "ClothingId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
