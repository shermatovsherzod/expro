using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class QuestionAnswerLike_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswerLikes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionAnswerLikes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPositive = table.Column<bool>(type: "bit", nullable: false),
                    QuestionAnswerID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerLikes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerLikes_QuestionAnswers_QuestionAnswerID",
                        column: x => x.QuestionAnswerID,
                        principalTable: "QuestionAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerLikes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerLikes_QuestionAnswerID",
                table: "QuestionAnswerLikes",
                column: "QuestionAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerLikes_UserID",
                table: "QuestionAnswerLikes",
                column: "UserID");
        }
    }
}
