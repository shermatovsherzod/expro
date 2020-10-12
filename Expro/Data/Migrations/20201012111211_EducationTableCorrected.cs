using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class EducationTableCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Educations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityOther",
                table: "Educations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Educations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Educations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Educations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Educations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Educations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CreatedBy",
                table: "Educations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ModifiedBy",
                table: "Educations",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_CreatedBy",
                table: "Educations",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_ModifiedBy",
                table: "Educations",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_CreatedBy",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_ModifiedBy",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CreatedBy",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_ModifiedBy",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CityOther",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Educations");
        }
    }
}
