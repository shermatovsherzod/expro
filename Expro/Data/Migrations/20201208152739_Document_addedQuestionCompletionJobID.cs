using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Document_addedQuestionCompletionJobID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "QuestionCompletionDeadline",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionCompletionJobID",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCompletionDeadline",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "QuestionCompletionJobID",
                table: "Documents");
        }
    }
}
