using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Vacancy_addedContactFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "Vacancies");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Vacancies",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vacancies",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Vacancies",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Vacancies");

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "Vacancies",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
