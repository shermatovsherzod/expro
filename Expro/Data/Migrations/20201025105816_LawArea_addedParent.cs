using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class LawArea_addedParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "LawAreas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_ParentID",
                table: "LawAreas",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_LawAreas_ParentID",
                table: "LawAreas",
                column: "ParentID",
                principalTable: "LawAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_LawAreas_ParentID",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_ParentID",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "LawAreas");
        }
    }
}
