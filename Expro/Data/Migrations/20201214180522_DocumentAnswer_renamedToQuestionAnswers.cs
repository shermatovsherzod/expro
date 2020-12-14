using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswer_renamedToQuestionAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_Attachments_AttachmentID",
                table: "DocumentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_AspNetUsers_CreatedBy",
                table: "DocumentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_AspNetUsers_ModifiedBy",
                table: "DocumentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_Question_QuestionID",
                table: "DocumentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerComment_DocumentAnswers_QuestionAnswerID",
                table: "QuestionAnswerComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentAnswers",
                table: "DocumentAnswers");

            migrationBuilder.RenameTable(
                name: "DocumentAnswers",
                newName: "QuestionAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswers_QuestionID",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswers_ModifiedBy",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswers_CreatedBy",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentAnswers_AttachmentID",
                table: "QuestionAnswers",
                newName: "IX_QuestionAnswers_AttachmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                column: "QuestionAnswerID",
                principalTable: "QuestionAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerComment_QuestionAnswers_QuestionAnswerID",
                table: "QuestionAnswerComment",
                column: "QuestionAnswerID",
                principalTable: "QuestionAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_Attachments_AttachmentID",
                table: "QuestionAnswers",
                column: "AttachmentID",
                principalTable: "Attachments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_AspNetUsers_CreatedBy",
                table: "QuestionAnswers",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_AspNetUsers_ModifiedBy",
                table: "QuestionAnswers",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_Question_QuestionID",
                table: "QuestionAnswers",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_QuestionAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerComment_QuestionAnswers_QuestionAnswerID",
                table: "QuestionAnswerComment");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_Attachments_AttachmentID",
                table: "QuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_AspNetUsers_CreatedBy",
                table: "QuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_AspNetUsers_ModifiedBy",
                table: "QuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_Question_QuestionID",
                table: "QuestionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionAnswers",
                table: "QuestionAnswers");

            migrationBuilder.RenameTable(
                name: "QuestionAnswers",
                newName: "DocumentAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_QuestionID",
                table: "DocumentAnswers",
                newName: "IX_DocumentAnswers_QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_ModifiedBy",
                table: "DocumentAnswers",
                newName: "IX_DocumentAnswers_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_CreatedBy",
                table: "DocumentAnswers",
                newName: "IX_DocumentAnswers_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAnswers_AttachmentID",
                table: "DocumentAnswers",
                newName: "IX_DocumentAnswers_AttachmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentAnswers",
                table: "DocumentAnswers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                column: "QuestionAnswerID",
                principalTable: "DocumentAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_Attachments_AttachmentID",
                table: "DocumentAnswers",
                column: "AttachmentID",
                principalTable: "Attachments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_AspNetUsers_CreatedBy",
                table: "DocumentAnswers",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_AspNetUsers_ModifiedBy",
                table: "DocumentAnswers",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_Question_QuestionID",
                table: "DocumentAnswers",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerComment_DocumentAnswers_QuestionAnswerID",
                table: "QuestionAnswerComment",
                column: "QuestionAnswerID",
                principalTable: "DocumentAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
