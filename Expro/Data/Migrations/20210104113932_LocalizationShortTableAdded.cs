using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class LocalizationShortTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserStatuses");

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "UserStatuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NameShortID",
                table: "UserStatuses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocalizationShort",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextRu = table.Column<string>(maxLength: 256, nullable: false),
                    TextUz = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationShort", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatuses_NameShortID",
                table: "UserStatuses",
                column: "NameShortID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatuses_LocalizationShort_NameShortID",
                table: "UserStatuses",
                column: "NameShortID",
                principalTable: "LocalizationShort",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatuses_LocalizationShort_NameShortID",
                table: "UserStatuses");

            migrationBuilder.DropTable(
                name: "LocalizationShort");

            migrationBuilder.DropIndex(
                name: "IX_UserStatuses_NameShortID",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "NameShortID",
                table: "UserStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserStatuses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
