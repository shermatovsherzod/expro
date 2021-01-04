using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class UserStatus_addedNameShortNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses");

            migrationBuilder.AlterColumn<int>(
                name: "NameID",
                table: "UserStatuses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses");

            migrationBuilder.AlterColumn<int>(
                name: "NameID",
                table: "UserStatuses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
