using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_bpm_core7_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleIdInOwnerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthRoleId",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_AuthRoleId",
                table: "Owners",
                column: "AuthRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AuthRoles_AuthRoleId",
                table: "Owners",
                column: "AuthRoleId",
                principalTable: "AuthRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AuthRoles_AuthRoleId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_AuthRoleId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "AuthRoleId",
                table: "Owners");
        }
    }
}
