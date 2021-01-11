using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBanHang.Migrations
{
    public partial class updateProductPropertyTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(nullable: true),
                    ReferenceId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsShow = table.Column<bool>(nullable: true),
                    ImgName = table.Column<string>(nullable: true),
                    OrderBy = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductColorSizes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeColor = table.Column<string>(nullable: true),
                    NameColor = table.Column<string>(nullable: true),
                    CodeSize = table.Column<string>(nullable: true),
                    NameSize = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Quantity_Saled = table.Column<int>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: false),
                    UnitPriceNew = table.Column<double>(nullable: false),
                    UnitPriceNewOld = table.Column<double>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<long>(nullable: false),
                    IDRelate = table.Column<long>(nullable: false),
                    TypeRelate = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ProductColorSizes");

            migrationBuilder.DropTable(
                name: "Relatives");
        }
    }
}
