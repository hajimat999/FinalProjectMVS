using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class vvm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Clothes_ClothingId",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Clothes_ClothingId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_ClothingId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Colors_ClothingId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Category_Image",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "Sizes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClothingColors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(nullable: false),
                    ClothingId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingColors_Clothes_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingColors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeId = table.Column<int>(nullable: false),
                    ClothingId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingSizes_Clothes_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothingSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingColors_ClothingId",
                table: "ClothingColors",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingColors_ColorId",
                table: "ClothingColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingSizes_ClothingId",
                table: "ClothingSizes",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingSizes_SizeId",
                table: "ClothingSizes",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingColors");

            migrationBuilder.DropTable(
                name: "ClothingSizes");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "Sizes");

            migrationBuilder.AddColumn<int>(
                name: "ClothingId",
                table: "Sizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClothingId",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category_Image",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ClothingId",
                table: "Sizes",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ClothingId",
                table: "Colors",
                column: "ClothingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Clothes_ClothingId",
                table: "Colors",
                column: "ClothingId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Clothes_ClothingId",
                table: "Sizes",
                column: "ClothingId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
