using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class PropertyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyColour = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 226, 36, 212, 7, 78, 222, 240, 208, 11, 90, 112, 3, 131, 18, 137, 170, 168, 216, 237, 138, 110, 172, 73, 42, 196, 6, 38, 82, 135, 251, 6, 128, 172, 43, 78, 187, 114, 72, 252, 223, 112, 206, 73, 14, 96, 38, 148, 41, 207, 43, 31, 116, 24, 175, 105, 39, 78, 233, 10, 190, 29, 161, 7, 37 }, new byte[] { 106, 191, 88, 247, 218, 238, 184, 250, 231, 251, 52, 253, 77, 61, 214, 106, 141, 222, 30, 219, 154, 143, 65, 92, 221, 19, 65, 176, 36, 148, 101, 15, 30, 139, 246, 45, 95, 238, 240, 87, 164, 22, 150, 175, 55, 220, 152, 212, 156, 132, 123, 82, 216, 164, 199, 128, 166, 81, 204, 99, 252, 210, 58, 34, 111, 207, 212, 22, 88, 12, 103, 134, 186, 78, 158, 165, 92, 37, 247, 63, 127, 68, 210, 226, 50, 48, 64, 229, 44, 80, 223, 58, 42, 74, 35, 146, 61, 134, 119, 178, 20, 98, 8, 113, 94, 201, 120, 168, 226, 155, 102, 138, 137, 213, 173, 24, 232, 88, 187, 222, 190, 54, 84, 155, 6, 164, 142, 148 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 191, 73, 173, 144, 98, 178, 239, 81, 94, 192, 49, 171, 132, 13, 137, 39, 99, 32, 235, 206, 12, 190, 211, 4, 90, 188, 82, 194, 55, 210, 4, 215, 154, 95, 240, 158, 182, 214, 117, 109, 172, 249, 252, 71, 170, 1, 148, 230, 149, 100, 89, 255, 196, 114, 210, 117, 182, 184, 93, 158, 177, 213, 247, 232 }, new byte[] { 72, 40, 156, 211, 252, 102, 40, 89, 103, 97, 107, 71, 2, 204, 180, 188, 176, 67, 72, 10, 169, 141, 138, 7, 91, 188, 247, 251, 154, 83, 158, 18, 118, 40, 81, 5, 207, 245, 75, 150, 184, 12, 58, 55, 163, 0, 244, 158, 191, 201, 112, 151, 169, 140, 167, 85, 205, 37, 176, 219, 113, 250, 57, 134, 44, 145, 134, 211, 215, 198, 123, 73, 73, 50, 15, 42, 144, 207, 156, 222, 126, 171, 165, 150, 3, 205, 211, 34, 54, 53, 141, 28, 54, 189, 7, 97, 9, 191, 216, 59, 213, 44, 24, 111, 161, 142, 55, 219, 225, 165, 171, 131, 72, 172, 208, 107, 215, 65, 128, 195, 142, 64, 76, 67, 255, 28, 111, 132 } });
        }
    }
}
