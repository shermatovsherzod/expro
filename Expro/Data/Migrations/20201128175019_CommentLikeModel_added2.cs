using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class CommentLikeModel_added2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_Comments_CommentID",
                table: "CommentLike");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_AspNetUsers_UserID",
                table: "CommentLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLike",
                table: "CommentLike");

            migrationBuilder.RenameTable(
                name: "CommentLike",
                newName: "CommentLikes");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLike_UserID",
                table: "CommentLikes",
                newName: "IX_CommentLikes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLike_CommentID",
                table: "CommentLikes",
                newName: "IX_CommentLikes_CommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Comments_CommentID",
                table: "CommentLikes",
                column: "CommentID",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserID",
                table: "CommentLikes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Comments_CommentID",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserID",
                table: "CommentLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentLikes",
                table: "CommentLikes");

            migrationBuilder.RenameTable(
                name: "CommentLikes",
                newName: "CommentLike");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_UserID",
                table: "CommentLike",
                newName: "IX_CommentLike_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_CommentLikes_CommentID",
                table: "CommentLike",
                newName: "IX_CommentLike_CommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentLike",
                table: "CommentLike",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_Comments_CommentID",
                table: "CommentLike",
                column: "CommentID",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_AspNetUsers_UserID",
                table: "CommentLike",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
