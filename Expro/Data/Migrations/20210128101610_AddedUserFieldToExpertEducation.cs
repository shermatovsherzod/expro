using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class AddedUserFieldToExpertEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "ExpertEducations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEducations_UserID",
                table: "ExpertEducations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertEducations_AspNetUsers_UserID",
                table: "ExpertEducations",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertEducations_AspNetUsers_UserID",
                table: "ExpertEducations");

            migrationBuilder.DropIndex(
                name: "IX_ExpertEducations_UserID",
                table: "ExpertEducations");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ExpertEducations");
        }
    }
}
