using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBanHang.Migrations
{
    public partial class shopdata001_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentCategoryID = table.Column<int>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    UpdateLBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ShortDecription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: true),
                    UnitPriceNew = table.Column<double>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: true),
                    IsNew = table.Column<bool>(nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    SupplierID = table.Column<int>(nullable: true),
                    SupplierName = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDelected = table.Column<bool>(nullable: true),
                    CT_WarrantyTimeID = table.Column<int>(nullable: true),
                    CT_WarrantyTimeName = table.Column<string>(nullable: true),
                    CT_MaterialID = table.Column<int>(nullable: true),
                    CT_MaterialName = table.Column<string>(nullable: true),
                    SizeName = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    UpdateLBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
