using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBanHang.Migrations
{
    public partial class _20201021_addprodutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: true),
                    UnitPriceNew = table.Column<double>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: true),
                    IsNew = table.Column<bool>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
