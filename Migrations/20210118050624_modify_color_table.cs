using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Project.Migrations
{
    public partial class modify_color_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CT_Colors",
                table: "CT_Colors");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "CT_Colors");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "CT_Colors",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CT_Colors",
                table: "CT_Colors",
                column: "ColorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CT_Colors",
                table: "CT_Colors");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "CT_Colors");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "CT_Colors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CT_Colors",
                table: "CT_Colors",
                column: "ID");
        }
    }
}
