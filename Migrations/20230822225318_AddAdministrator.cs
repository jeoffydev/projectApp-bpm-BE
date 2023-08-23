using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthRoleId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_AuthRoles_AuthRoleId",
                        column: x => x.AuthRoleId,
                        principalTable: "AuthRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 41, 49, 0, 23, 105, 39, 96, 211, 225, 177, 129, 237, 32, 162, 68, 227, 190, 17, 144, 195, 10, 122, 67, 171, 134, 19, 168, 39, 153, 71, 137, 57, 234, 240, 25, 106, 236, 104, 154, 111, 80, 122, 248, 194, 150, 105, 192, 254, 94, 17, 105, 198, 248, 9, 56, 61, 188, 206, 107, 56, 73, 143, 42, 138 }, new byte[] { 19, 243, 165, 181, 7, 235, 138, 228, 180, 167, 243, 50, 50, 243, 177, 22, 162, 25, 151, 28, 52, 208, 152, 119, 229, 102, 128, 130, 72, 67, 229, 10, 177, 160, 200, 119, 245, 210, 176, 121, 236, 207, 181, 102, 93, 69, 240, 193, 252, 78, 189, 196, 194, 123, 189, 131, 193, 64, 87, 106, 204, 0, 132, 107, 241, 125, 14, 145, 208, 244, 63, 69, 57, 32, 177, 120, 24, 159, 228, 72, 40, 189, 42, 183, 65, 239, 214, 79, 186, 118, 233, 239, 150, 227, 240, 187, 87, 189, 105, 177, 117, 71, 4, 2, 162, 248, 125, 219, 86, 252, 97, 113, 32, 109, 139, 108, 87, 244, 225, 177, 161, 2, 208, 248, 45, 170, 78, 140 } });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_AuthRoleId",
                table: "Administrators",
                column: "AuthRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.UpdateData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 195, 77, 50, 29, 96, 172, 99, 85, 6, 254, 74, 178, 123, 227, 131, 228, 4, 75, 120, 7, 23, 213, 171, 214, 119, 98, 10, 183, 22, 165, 71, 179, 5, 49, 233, 18, 67, 226, 242, 181, 139, 55, 54, 170, 78, 68, 124, 18, 110, 63, 95, 233, 13, 196, 57, 218, 204, 53, 61, 64, 147, 92, 81, 252 }, new byte[] { 205, 199, 164, 97, 73, 197, 39, 141, 27, 20, 72, 162, 212, 31, 90, 151, 112, 38, 201, 118, 33, 14, 1, 93, 193, 84, 19, 240, 43, 216, 240, 3, 103, 3, 74, 201, 36, 94, 203, 125, 113, 129, 132, 253, 97, 205, 73, 195, 61, 172, 152, 16, 189, 12, 242, 230, 134, 209, 99, 250, 194, 218, 40, 57, 33, 254, 36, 220, 105, 53, 103, 13, 214, 19, 248, 234, 126, 104, 242, 57, 75, 62, 124, 162, 113, 212, 126, 121, 116, 243, 116, 86, 86, 200, 172, 218, 132, 181, 173, 88, 35, 207, 17, 42, 149, 233, 162, 134, 174, 140, 15, 152, 211, 59, 170, 49, 117, 63, 119, 23, 127, 32, 154, 224, 242, 2, 32, 144 } });
        }
    }
}
