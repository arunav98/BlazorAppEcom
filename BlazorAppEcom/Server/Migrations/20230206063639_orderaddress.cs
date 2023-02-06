using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppEcom.Server.Migrations
{
    public partial class orderaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_addressId",
                table: "Orders",
                column: "addressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_addressId",
                table: "Orders",
                column: "addressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_addressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_addressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "addressId",
                table: "Orders");
        }
    }
}
