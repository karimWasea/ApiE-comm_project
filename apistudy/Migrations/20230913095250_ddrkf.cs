using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Api.Migrations
{
    /// <inheritdoc />
    public partial class ddrkf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Productid",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Productid",
                table: "OrderDetails",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_Productid",
                table: "OrderDetails",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_Productid",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_Productid",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "OrderDetails");
        }
    }
}
