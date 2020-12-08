using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Document_addedQuestionIsCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateQuestionCompleted",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QuestionIsCompleted",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaidFee",
                table: "DocumentAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateQuestionCompleted",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "QuestionIsCompleted",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "PaidFee",
                table: "DocumentAnswers");
        }
    }
}
