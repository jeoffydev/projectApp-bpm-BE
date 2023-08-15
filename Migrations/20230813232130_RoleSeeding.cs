using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AuthRoles",
                columns: new[] { "Id", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "The owner with highest permission on this app", "Owner" },
                    { 2, "The Customer administrator and has high permission", "Admin" },
                    { 3, "Limited access in the customers private pages", "Member" },
                    { 4, "Static and read only access", "Contractor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuthRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuthRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AuthRoles",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
