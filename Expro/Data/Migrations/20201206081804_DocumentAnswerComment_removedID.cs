using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswerComment_removedID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentAnswerComment",
                table: "DocumentAnswerComment");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAnswerComment_DocumentAnswerID",
                table: "DocumentAnswerComment");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "DocumentAnswerComment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentAnswerComment",
                table: "DocumentAnswerComment",
                columns: new[] { "DocumentAnswerID", "CommentID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentAnswerComment",
                table: "DocumentAnswerComment");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "DocumentAnswerComment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentAnswerComment",
                table: "DocumentAnswerComment",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswerComment_DocumentAnswerID",
                table: "DocumentAnswerComment",
                column: "DocumentAnswerID");
        }
    }
}
