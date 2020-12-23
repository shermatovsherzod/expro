using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class QuestionAnswerLike_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionAnswerLike",
                columns: table => new
                {
                    QuestionAnswerID = table.Column<int>(nullable: false),
                    LikeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerLike", x => new { x.QuestionAnswerID, x.LikeID });
                    table.ForeignKey(
                        name: "FK_QuestionAnswerLike_Likes_LikeID",
                        column: x => x.LikeID,
                        principalTable: "Likes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerLike_QuestionAnswers_QuestionAnswerID",
                        column: x => x.QuestionAnswerID,
                        principalTable: "QuestionAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerLike_LikeID",
                table: "QuestionAnswerLike",
                column: "LikeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswerLike");
        }
    }
}
