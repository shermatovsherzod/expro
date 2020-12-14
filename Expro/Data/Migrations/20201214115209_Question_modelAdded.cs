using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Question_modelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_DocumentAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_Documents_DocumentID",
                table: "DocumentAnswers");

            migrationBuilder.DropTable(
                name: "DocumentAnswerComment");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAnswers_DocumentID",
                table: "DocumentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAnswerLikes_DocumentAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropColumn(
                name: "DocumentID",
                table: "DocumentAnswers");

            migrationBuilder.DropColumn(
                name: "DocumentAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.AddColumn<int>(
                name: "QuestionID",
                table: "DocumentAnswers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionAnswerID",
                table: "DocumentAnswerLikes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DocumentStatusID = table.Column<int>(nullable: false),
                    DateSubmittedForApproval = table.Column<DateTime>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    RejectionDeadline = table.Column<DateTime>(nullable: true),
                    RejectionJobID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 1024, nullable: false),
                    AttachmentID = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    PriceType = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: true),
                    NumberOfViews = table.Column<int>(nullable: false),
                    NumberOfPurchases = table.Column<int>(nullable: false),
                    QuestionCompletionDeadline = table.Column<DateTime>(nullable: true),
                    QuestionCompletionJobID = table.Column<string>(nullable: true),
                    QuestionIsCompleted = table.Column<bool>(nullable: true),
                    QuestionFeeIsDistributed = table.Column<bool>(nullable: true),
                    DateQuestionCompleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Question_Attachments_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_DocumentStatuses_DocumentStatusID",
                        column: x => x.DocumentStatusID,
                        principalTable: "DocumentStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerComment",
                columns: table => new
                {
                    QuestionAnswerID = table.Column<int>(nullable: false),
                    CommentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerComment", x => new { x.QuestionAnswerID, x.CommentID });
                    table.ForeignKey(
                        name: "FK_QuestionAnswerComment_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerComment_DocumentAnswers_QuestionAnswerID",
                        column: x => x.QuestionAnswerID,
                        principalTable: "DocumentAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionLawArea",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false),
                    LawAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLawArea", x => new { x.QuestionID, x.LawAreaID });
                    table.ForeignKey(
                        name: "FK_QuestionLawArea_LawAreas_LawAreaID",
                        column: x => x.LawAreaID,
                        principalTable: "LawAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionLawArea_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_QuestionID",
                table: "DocumentAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerLikes_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                column: "QuestionAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AttachmentID",
                table: "Question",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CreatedBy",
                table: "Question",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Question_DocumentStatusID",
                table: "Question",
                column: "DocumentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ModifiedBy",
                table: "Question",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerComment_CommentID",
                table: "QuestionAnswerComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionLawArea_LawAreaID",
                table: "QuestionLawArea",
                column: "LawAreaID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes",
                column: "QuestionAnswerID",
                principalTable: "DocumentAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_Question_QuestionID",
                table: "DocumentAnswers",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAnswers_Question_QuestionID",
                table: "DocumentAnswers");

            migrationBuilder.DropTable(
                name: "QuestionAnswerComment");

            migrationBuilder.DropTable(
                name: "QuestionLawArea");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAnswers_QuestionID",
                table: "DocumentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAnswerLikes_QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.DropColumn(
                name: "QuestionID",
                table: "DocumentAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionAnswerID",
                table: "DocumentAnswerLikes");

            migrationBuilder.AddColumn<int>(
                name: "DocumentID",
                table: "DocumentAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DocumentAnswerID",
                table: "DocumentAnswerLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DocumentAnswerComment",
                columns: table => new
                {
                    DocumentAnswerID = table.Column<int>(type: "int", nullable: false),
                    CommentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAnswerComment", x => new { x.DocumentAnswerID, x.CommentID });
                    table.ForeignKey(
                        name: "FK_DocumentAnswerComment_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAnswerComment_DocumentAnswers_DocumentAnswerID",
                        column: x => x.DocumentAnswerID,
                        principalTable: "DocumentAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_DocumentID",
                table: "DocumentAnswers",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerLikes_DocumentAnswerID",
                table: "DocumentAnswerLikes",
                column: "DocumentAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerComment_CommentID",
                table: "DocumentAnswerComment",
                column: "CommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswerLikes_DocumentAnswers_DocumentAnswerID",
                table: "DocumentAnswerLikes",
                column: "DocumentAnswerID",
                principalTable: "DocumentAnswers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAnswers_Documents_DocumentID",
                table: "DocumentAnswers",
                column: "DocumentID",
                principalTable: "Documents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
