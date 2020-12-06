using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Comment_addedParentCommentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentID",
                table: "Comments",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentID",
                table: "Comments",
                column: "ParentID",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Comments");
        }
    }
}
