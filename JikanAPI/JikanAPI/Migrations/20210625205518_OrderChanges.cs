using Microsoft.EntityFrameworkCore.Migrations;

namespace JikanAPI.Migrations
{
    public partial class OrderChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watches_Orders_OrderId",
                table: "Watches");

            migrationBuilder.DropIndex(
                name: "IX_Watches_OrderId",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Watches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Watches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Watches_OrderId",
                table: "Watches",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watches_Orders_OrderId",
                table: "Watches",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
