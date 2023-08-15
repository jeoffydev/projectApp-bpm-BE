using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Active", "AuthRoleId", "Email", "FullName", "PasswordHash", "PasswordSalt", "SecretKey" },
                values: new object[] { 1001, true, 1, "jeoffy_hipolito@yahoo.com", "Jeoffy Hipolito", new byte[] { 149, 88, 216, 29, 191, 42, 26, 16, 215, 57, 14, 245, 248, 157, 66, 131, 179, 12, 201, 148, 36, 170, 208, 246, 89, 79, 134, 136, 107, 1, 9, 124, 64, 254, 44, 52, 210, 105, 81, 181, 9, 86, 159, 160, 34, 38, 200, 203, 80, 84, 87, 117, 206, 232, 106, 131, 192, 147, 192, 157, 27, 87, 82, 175 }, new byte[] { 17, 88, 197, 108, 219, 3, 190, 174, 93, 220, 218, 118, 170, 243, 250, 197, 191, 13, 94, 129, 152, 234, 151, 96, 128, 150, 166, 163, 219, 230, 255, 153, 250, 214, 6, 242, 175, 70, 98, 16, 166, 152, 112, 40, 149, 143, 243, 39, 118, 116, 225, 250, 2, 19, 166, 214, 11, 85, 116, 94, 137, 86, 124, 54, 136, 223, 52, 160, 220, 117, 154, 242, 184, 75, 169, 65, 17, 231, 178, 66, 106, 80, 91, 88, 76, 46, 78, 94, 162, 57, 132, 197, 137, 104, 52, 209, 47, 63, 235, 179, 67, 225, 250, 151, 251, 81, 156, 157, 220, 78, 141, 6, 45, 226, 170, 200, 189, 59, 55, 217, 92, 34, 154, 249, 6, 50, 81, 253 }, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001);
        }
    }
}
