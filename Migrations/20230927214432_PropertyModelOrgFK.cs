using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class PropertyModelOrgFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 184, 159, 37, 168, 104, 81, 37, 219, 138, 101, 45, 203, 94, 8, 212, 56, 28, 125, 87, 141, 209, 193, 12, 43, 38, 197, 228, 122, 106, 112, 180, 233, 44, 82, 232, 139, 14, 238, 6, 152, 134, 236, 56, 118, 186, 113, 244, 177, 171, 110, 217, 167, 84, 32, 185, 101, 166, 94, 180, 123, 67, 15, 224, 210 }, new byte[] { 158, 179, 84, 204, 64, 114, 183, 37, 168, 214, 143, 74, 161, 36, 67, 19, 153, 230, 240, 130, 90, 101, 239, 252, 6, 134, 219, 183, 16, 187, 65, 122, 238, 39, 183, 145, 55, 203, 182, 120, 233, 73, 92, 255, 151, 77, 29, 115, 152, 232, 44, 99, 165, 98, 1, 29, 16, 132, 52, 48, 55, 176, 38, 157, 201, 95, 104, 93, 74, 214, 176, 171, 86, 3, 230, 135, 87, 78, 60, 230, 254, 209, 81, 254, 45, 213, 115, 115, 194, 133, 23, 36, 74, 183, 117, 162, 201, 247, 234, 239, 66, 136, 13, 54, 200, 56, 32, 121, 202, 204, 41, 13, 239, 238, 245, 42, 57, 109, 207, 86, 170, 125, 164, 110, 160, 52, 29, 111 } });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OrganizationId",
                table: "Properties",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Organizations_OrganizationId",
                table: "Properties",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Organizations_OrganizationId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OrganizationId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Properties");

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 36, 212, 7, 78, 222, 240, 208, 11, 90, 112, 3, 131, 18, 137, 170, 168, 216, 237, 138, 110, 172, 73, 42, 196, 6, 38, 82, 135, 251, 6, 128, 172, 43, 78, 187, 114, 72, 252, 223, 112, 206, 73, 14, 96, 38, 148, 41, 207, 43, 31, 116, 24, 175, 105, 39, 78, 233, 10, 190, 29, 161, 7, 37 }, new byte[] { 106, 191, 88, 247, 218, 238, 184, 250, 231, 251, 52, 253, 77, 61, 214, 106, 141, 222, 30, 219, 154, 143, 65, 92, 221, 19, 65, 176, 36, 148, 101, 15, 30, 139, 246, 45, 95, 238, 240, 87, 164, 22, 150, 175, 55, 220, 152, 212, 156, 132, 123, 82, 216, 164, 199, 128, 166, 81, 204, 99, 252, 210, 58, 34, 111, 207, 212, 22, 88, 12, 103, 134, 186, 78, 158, 165, 92, 37, 247, 63, 127, 68, 210, 226, 50, 48, 64, 229, 44, 80, 223, 58, 42, 74, 35, 146, 61, 134, 119, 178, 20, 98, 8, 113, 94, 201, 120, 168, 226, 155, 102, 138, 137, 213, 173, 24, 232, 88, 187, 222, 190, 54, 84, 155, 6, 164, 142, 148 } });
        }
    }
}
