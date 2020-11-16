using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class UserApproveStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApproveStatus",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmittedForApproval",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateSubmittedForApproval",
                table: "AspNetUsers");
        }
    }
}
