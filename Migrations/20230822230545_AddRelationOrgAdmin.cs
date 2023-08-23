using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationOrgAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 191, 73, 173, 144, 98, 178, 239, 81, 94, 192, 49, 171, 132, 13, 137, 39, 99, 32, 235, 206, 12, 190, 211, 4, 90, 188, 82, 194, 55, 210, 4, 215, 154, 95, 240, 158, 182, 214, 117, 109, 172, 249, 252, 71, 170, 1, 148, 230, 149, 100, 89, 255, 196, 114, 210, 117, 182, 184, 93, 158, 177, 213, 247, 232 }, new byte[] { 72, 40, 156, 211, 252, 102, 40, 89, 103, 97, 107, 71, 2, 204, 180, 188, 176, 67, 72, 10, 169, 141, 138, 7, 91, 188, 247, 251, 154, 83, 158, 18, 118, 40, 81, 5, 207, 245, 75, 150, 184, 12, 58, 55, 163, 0, 244, 158, 191, 201, 112, 151, 169, 140, 167, 85, 205, 37, 176, 219, 113, 250, 57, 134, 44, 145, 134, 211, 215, 198, 123, 73, 73, 50, 15, 42, 144, 207, 156, 222, 126, 171, 165, 150, 3, 205, 211, 34, 54, 53, 141, 28, 54, 189, 7, 97, 9, 191, 216, 59, 213, 44, 24, 111, 161, 142, 55, 219, 225, 165, 171, 131, 72, 172, 208, 107, 215, 65, 128, 195, 142, 64, 76, 67, 255, 28, 111, 132 } });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_OrganizationId",
                table: "Administrators",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Organizations_OrganizationId",
                table: "Administrators",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Organizations_OrganizationId",
                table: "Administrators");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_OrganizationId",
                table: "Administrators");

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 41, 49, 0, 23, 105, 39, 96, 211, 225, 177, 129, 237, 32, 162, 68, 227, 190, 17, 144, 195, 10, 122, 67, 171, 134, 19, 168, 39, 153, 71, 137, 57, 234, 240, 25, 106, 236, 104, 154, 111, 80, 122, 248, 194, 150, 105, 192, 254, 94, 17, 105, 198, 248, 9, 56, 61, 188, 206, 107, 56, 73, 143, 42, 138 }, new byte[] { 19, 243, 165, 181, 7, 235, 138, 228, 180, 167, 243, 50, 50, 243, 177, 22, 162, 25, 151, 28, 52, 208, 152, 119, 229, 102, 128, 130, 72, 67, 229, 10, 177, 160, 200, 119, 245, 210, 176, 121, 236, 207, 181, 102, 93, 69, 240, 193, 252, 78, 189, 196, 194, 123, 189, 131, 193, 64, 87, 106, 204, 0, 132, 107, 241, 125, 14, 145, 208, 244, 63, 69, 57, 32, 177, 120, 24, 159, 228, 72, 40, 189, 42, 183, 65, 239, 214, 79, 186, 118, 233, 239, 150, 227, 240, 187, 87, 189, 105, 177, 117, 71, 4, 2, 162, 248, 125, 219, 86, 252, 97, 113, 32, 109, 139, 108, 87, 244, 225, 177, 161, 2, 208, 248, 45, 170, 78, 140 } });
        }
    }
}
