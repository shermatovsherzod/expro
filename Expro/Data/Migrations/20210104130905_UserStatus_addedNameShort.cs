using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class UserStatus_addedNameShort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "UserStatuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatuses_NameID",
                table: "UserStatuses",
                column: "NameID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserStatuses_NameID",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "UserStatuses");
        }
    }
}
