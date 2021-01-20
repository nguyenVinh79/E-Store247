using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Project.Migrations
{
    public partial class addcolumn_propertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "ProductColorSizes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductColorSizes");
        }
    }
}
