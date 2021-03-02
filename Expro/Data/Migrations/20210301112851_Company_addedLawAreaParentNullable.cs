using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Company_addedLawAreaParentNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LawAreaParentID",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LawAreaParentID",
                table: "Companies",
                column: "LawAreaParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_LawAreas_LawAreaParentID",
                table: "Companies",
                column: "LawAreaParentID",
                principalTable: "LawAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_LawAreas_LawAreaParentID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LawAreaParentID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LawAreaParentID",
                table: "Companies");
        }
    }
}
