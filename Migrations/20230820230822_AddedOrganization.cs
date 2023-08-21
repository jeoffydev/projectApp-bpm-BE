using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 195, 77, 50, 29, 96, 172, 99, 85, 6, 254, 74, 178, 123, 227, 131, 228, 4, 75, 120, 7, 23, 213, 171, 214, 119, 98, 10, 183, 22, 165, 71, 179, 5, 49, 233, 18, 67, 226, 242, 181, 139, 55, 54, 170, 78, 68, 124, 18, 110, 63, 95, 233, 13, 196, 57, 218, 204, 53, 61, 64, 147, 92, 81, 252 }, new byte[] { 205, 199, 164, 97, 73, 197, 39, 141, 27, 20, 72, 162, 212, 31, 90, 151, 112, 38, 201, 118, 33, 14, 1, 93, 193, 84, 19, 240, 43, 216, 240, 3, 103, 3, 74, 201, 36, 94, 203, 125, 113, 129, 132, 253, 97, 205, 73, 195, 61, 172, 152, 16, 189, 12, 242, 230, 134, 209, 99, 250, 194, 218, 40, 57, 33, 254, 36, 220, 105, 53, 103, 13, 214, 19, 248, 234, 126, 104, 242, 57, 75, 62, 124, 162, 113, 212, 126, 121, 116, 243, 116, 86, 86, 200, 172, 218, 132, 181, 173, 88, 35, 207, 17, 42, 149, 233, 162, 134, 174, 140, 15, 152, 211, 59, 170, 49, 117, 63, 119, 23, 127, 32, 154, 224, 242, 2, 32, 144 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 149, 88, 216, 29, 191, 42, 26, 16, 215, 57, 14, 245, 248, 157, 66, 131, 179, 12, 201, 148, 36, 170, 208, 246, 89, 79, 134, 136, 107, 1, 9, 124, 64, 254, 44, 52, 210, 105, 81, 181, 9, 86, 159, 160, 34, 38, 200, 203, 80, 84, 87, 117, 206, 232, 106, 131, 192, 147, 192, 157, 27, 87, 82, 175 }, new byte[] { 17, 88, 197, 108, 219, 3, 190, 174, 93, 220, 218, 118, 170, 243, 250, 197, 191, 13, 94, 129, 152, 234, 151, 96, 128, 150, 166, 163, 219, 230, 255, 153, 250, 214, 6, 242, 175, 70, 98, 16, 166, 152, 112, 40, 149, 143, 243, 39, 118, 116, 225, 250, 2, 19, 166, 214, 11, 85, 116, 94, 137, 86, 124, 54, 136, 223, 52, 160, 220, 117, 154, 242, 184, 75, 169, 65, 17, 231, 178, 66, 106, 80, 91, 88, 76, 46, 78, 94, 162, 57, 132, 197, 137, 104, 52, 209, 47, 63, 235, 179, 67, 225, 250, 151, 251, 81, 156, 157, 220, 78, 141, 6, 45, 226, 170, 200, 189, 59, 55, 217, 92, 34, 154, 249, 6, 50, 81, 253 } });
        }
    }
}
