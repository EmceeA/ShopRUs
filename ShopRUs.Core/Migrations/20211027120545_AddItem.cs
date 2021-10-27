using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopRUs.Core.Migrations
{
    public partial class AddItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ItemAmount",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemAmount",
                table: "Items");
        }
    }
}
