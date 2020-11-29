using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class workexperienceRemovedUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_AspNetUsers_UserID",
                table: "WorkExperiences");

            migrationBuilder.DropIndex(
                name: "IX_WorkExperiences_UserID",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "WorkExperiences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "WorkExperiences",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_UserID",
                table: "WorkExperiences",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_AspNetUsers_UserID",
                table: "WorkExperiences",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
