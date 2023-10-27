using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Api.Migrations
{
    /// <inheritdoc />
    public partial class ddrk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "applicstionuser",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "applicstionuserid",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_applicstionuserid",
                table: "OrderDetails",
                column: "applicstionuserid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_applicstionuserid",
                table: "OrderDetails",
                column: "applicstionuserid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_applicstionuserid",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_applicstionuserid",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "applicstionuserid",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicstionuser",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
