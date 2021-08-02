using Microsoft.EntityFrameworkCore.Migrations;

namespace JikanAPI.Migrations
{
    public partial class NewOrderUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "PurchaserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PurchaserId",
                table: "Orders",
                column: "PurchaserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_PurchaserId",
                table: "Orders",
                column: "PurchaserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_PurchaserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PurchaserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PurchaserId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
