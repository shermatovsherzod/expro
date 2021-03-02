using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Question_addedLawAreaParentNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LawAreaParentID",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_LawAreaParentID",
                table: "Question",
                column: "LawAreaParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_LawAreas_LawAreaParentID",
                table: "Question",
                column: "LawAreaParentID",
                principalTable: "LawAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_LawAreas_LawAreaParentID",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_LawAreaParentID",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "LawAreaParentID",
                table: "Question");
        }
    }
}
