using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class FeedbackStatusTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmittedForApproval",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedbackStatusID",
                table: "Feedbacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RejectedReasonText",
                table: "Feedbacks",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedbackStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    NameID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FeedbackStatuses_LocalizationShorts_NameID",
                        column: x => x.NameID,
                        principalTable: "LocalizationShorts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FeedbackStatusID",
                table: "Feedbacks",
                column: "FeedbackStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackStatuses_NameID",
                table: "FeedbackStatuses",
                column: "NameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackStatuses_FeedbackStatusID",
                table: "Feedbacks",
                column: "FeedbackStatusID",
                principalTable: "FeedbackStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackStatuses_FeedbackStatusID",
                table: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FeedbackStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_FeedbackStatusID",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DateSubmittedForApproval",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackStatusID",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "RejectedReasonText",
                table: "Feedbacks");
        }
    }
}
