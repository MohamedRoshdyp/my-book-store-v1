using Microsoft.EntityFrameworkCore.Migrations;

namespace my_book_store_v1.Migrations
{
    public partial class UserRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee5b0f05-131e-4bba-8afb-a0e240fede33", "52b89607-b7ca-4a7a-a721-5a3d499cbd0b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "348e2aec-affa-4acb-8637-40e909d6f20b", "1d679f61-8f91-4b1c-b93e-398e5b1aecc3", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "348e2aec-affa-4acb-8637-40e909d6f20b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee5b0f05-131e-4bba-8afb-a0e240fede33");
        }
    }
}
