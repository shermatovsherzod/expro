using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class UserIDRemovedFromEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserID",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Educations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserID",
                table: "Educations",
                newName: "IX_Educations_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Educations",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                newName: "IX_Educations_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserID",
                table: "Educations",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
