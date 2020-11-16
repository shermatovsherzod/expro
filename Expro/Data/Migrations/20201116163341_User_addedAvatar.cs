using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class User_addedAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "ApproveStatus",
            //    table: "AspNetUsers",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvatarID",
                table: "AspNetUsers",
                nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateApproved",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateRejected",
            //    table: "AspNetUsers",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateSubmittedForApproval",
            //    table: "AspNetUsers",
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarID",
                table: "AspNetUsers",
                column: "AvatarID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Attachments_AvatarID",
                table: "AspNetUsers",
                column: "AvatarID",
                principalTable: "Attachments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Attachments_AvatarID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarID",
                table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "ApproveStatus",
            //    table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarID",
                table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "DateApproved",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "DateRejected",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "DateSubmittedForApproval",
            //    table: "AspNetUsers");
        }
    }
}
