using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAD3.Migrations
{
    /// <inheritdoc />
    public partial class Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Delivery_OrderId",
                table: "Delivery",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Order_OrderId",
                table: "Delivery",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Order_OrderId",
                table: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_OrderId",
                table: "Delivery");
        }
    }
}
