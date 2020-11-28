using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class removeUserIDFromEducation7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Educations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserID",
                table: "Educations",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserID",
                table: "Educations",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserID",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UserID",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Educations");
        }
    }
}
