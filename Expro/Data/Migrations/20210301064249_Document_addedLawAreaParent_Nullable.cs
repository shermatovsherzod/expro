using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Document_addedLawAreaParent_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LawAreaParentID",
                table: "Documents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LawAreaParentID",
                table: "Documents",
                column: "LawAreaParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_LawAreas_LawAreaParentID",
                table: "Documents",
                column: "LawAreaParentID",
                principalTable: "LawAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_LawAreas_LawAreaParentID",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_LawAreaParentID",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LawAreaParentID",
                table: "Documents");
        }
    }
}
