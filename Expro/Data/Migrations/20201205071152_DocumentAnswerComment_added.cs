using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswerComment_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAnswerComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentAnswerID = table.Column<int>(nullable: false),
                    CommentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAnswerComment", x => x.ID);
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
                name: "IX_DocumentAnswerComment_CommentID",
                table: "DocumentAnswerComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerComment_DocumentAnswerID",
                table: "DocumentAnswerComment",
                column: "DocumentAnswerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAnswerComment");
        }
    }
}
