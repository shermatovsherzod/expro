using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswerLike_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAnswerLikes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentAnswerID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAnswerLikes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentAnswerLikes_DocumentAnswers_DocumentAnswerID",
                        column: x => x.DocumentAnswerID,
                        principalTable: "DocumentAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAnswerLikes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerLikes_DocumentAnswerID",
                table: "DocumentAnswerLikes",
                column: "DocumentAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerLikes_UserID",
                table: "DocumentAnswerLikes",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAnswerLikes");
        }
    }
}
