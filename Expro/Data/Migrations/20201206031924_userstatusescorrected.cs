using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class userstatusescorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserStatuses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedReasonText",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStatusID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserStatusID",
                table: "AspNetUsers",
                column: "UserStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserStatuses_UserStatusID",
                table: "AspNetUsers",
                column: "UserStatusID",
                principalTable: "UserStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserStatuses_UserStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserStatusID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "RejectedReasonText",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserStatusID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ApproveStatus",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
