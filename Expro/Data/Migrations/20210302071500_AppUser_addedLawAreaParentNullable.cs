using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class AppUser_addedLawAreaParentNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LawAreaParentID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LawAreaParentID",
                table: "AspNetUsers",
                column: "LawAreaParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LawAreas_LawAreaParentID",
                table: "AspNetUsers",
                column: "LawAreaParentID",
                principalTable: "LawAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LawAreas_LawAreaParentID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LawAreaParentID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LawAreaParentID",
                table: "AspNetUsers");
        }
    }
}
