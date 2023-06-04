using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Database.Migrations
{
    public partial class OrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addres2",
                table: "Orders",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "Addres1",
                table: "Orders",
                newName: "Address1");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Orders",
                newName: "Addres2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Orders",
                newName: "Addres1");
        }
    }
}
