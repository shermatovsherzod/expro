using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswerLikes_renamedToQuestionAnswerLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_AspNetUsers_UserID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentAnswerLikes",
                table: "DocumentAnswerLikes");

            migrationBuilder.RenameTable(
                name: "DocumentAnswerLikes",
                newName: "QuestionAnswerLikes");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswerLikes_UserID",
                table: "QuestionAnswerLikes",
                newName: "IX_QuestionAnswerLikes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswerLikes_QuestionAnswerID",
                table: "QuestionAnswerLikes",
                newName: "IX_QuestionAnswerLikes_QuestionAnswerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswerLikes",
                table: "QuestionAnswerLikes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "QuestionAnswerLikes",
                column: "QuestionAnswerID",
                principalTable: "QuestionAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerLikes_AspNetUsers_UserID",
                table: "QuestionAnswerLikes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "QuestionAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerLikes_AspNetUsers_UserID",
                table: "QuestionAnswerLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswerLikes",
                table: "QuestionAnswerLikes");

            migrationBuilder.RenameTable(
                name: "QuestionAnswerLikes",
                newName: "DocumentAnswerLikes");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerLikes_UserID",
                table: "DocumentAnswerLikes",
                newName: "IX_DocumentAnswerLikes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswerLikes_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                newName: "IX_DocumentAnswerLikes_QuestionAnswerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentAnswerLikes",
                table: "DocumentAnswerLikes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                column: "QuestionAnswerID",
                principalTable: "QuestionAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_AspNetUsers_UserID",
                table: "DocumentAnswerLikes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
