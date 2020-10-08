using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class BaseModelAuthorableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genders_AspNetUsers_CreatedBy",
                table: "Genders");

            migrationBuilder.DropForeignKey(
                name: "FK_Genders_AspNetUsers_ModifiedBy",
                table: "Genders");

            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_AspNetUsers_CreatedBy",
                table: "LawAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_AspNetUsers_ModifiedBy",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_CreatedBy",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_ModifiedBy",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_Genders_CreatedBy",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Genders_ModifiedBy",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Genders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LawAreas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "LawAreas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "LawAreas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LawAreas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LawAreas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Genders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Genders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Genders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Genders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Genders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_CreatedBy",
                table: "LawAreas",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_ModifiedBy",
                table: "LawAreas",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_CreatedBy",
                table: "Genders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_ModifiedBy",
                table: "Genders",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_AspNetUsers_CreatedBy",
                table: "Genders",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_AspNetUsers_ModifiedBy",
                table: "Genders",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_AspNetUsers_CreatedBy",
                table: "LawAreas",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_AspNetUsers_ModifiedBy",
                table: "LawAreas",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
