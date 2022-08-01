using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class create7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Color_ColorId",
                table: "Clothes");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_ColorId",
                table: "Clothes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "Color_Id",
                table: "Clothes");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colors");

            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClothingId",
                table: "Colors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ClothingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Clothes_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ClothingId",
                table: "Colors",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ClothingId",
                table: "Sizes",
                column: "ClothingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Clothes_ClothingId",
                table: "Colors",
                column: "ClothingId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Clothes_ClothingId",
                table: "Colors");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_ClothingId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Color");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Clothes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Color_Id",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "Id");

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
    }
}
