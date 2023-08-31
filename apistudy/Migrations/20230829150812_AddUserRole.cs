using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apistudy.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");














            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
            migrationBuilder.InsertData(
       table: "AspNetRoles",
       columns: new[] { "Id", "Name", "NormalizedName" },
       values: new object[,]
       {
        { Guid.NewGuid().ToString(), "User", "User".ToUpper(), },
           // Add more rows if needed
       });



            migrationBuilder.InsertData(
       table: "AspNetRoles",
       columns: new[] { "Id", "Name", "NormalizedName" },
       values: new object[,]
       {
        { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper()},
           // Add more rows if needed
       });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");



            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");







        }
    }
}
