using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class ApplicationUser_addedRejectionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RejectionDeadline",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionJobID",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionDeadline",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RejectionJobID",
                table: "Documents");
        }
    }
}
