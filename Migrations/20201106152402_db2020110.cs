using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBanHang.Migrations
{
    public partial class db2020110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelected",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelected",
                table: "Products",
                type: "bit",
                nullable: true);
        }
    }
}
