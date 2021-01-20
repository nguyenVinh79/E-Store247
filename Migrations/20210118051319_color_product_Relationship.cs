using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Project.Migrations
{
    public partial class color_product_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductColorSizes_ColorId",
                table: "ProductColorSizes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColorSizes_CT_Colors_ColorId",
                table: "ProductColorSizes",
                column: "ColorId",
                principalTable: "CT_Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColorSizes_CT_Colors_ColorId",
                table: "ProductColorSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductColorSizes_ColorId",
                table: "ProductColorSizes");
        }
    }
}
