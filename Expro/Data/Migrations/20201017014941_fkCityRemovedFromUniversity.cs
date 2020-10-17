using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class fkCityRemovedFromUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Cities_CityID",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CityID",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CityOther",
                table: "Educations");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Educations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Educations");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityOther",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CityID",
                table: "Educations",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Cities_CityID",
                table: "Educations",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
