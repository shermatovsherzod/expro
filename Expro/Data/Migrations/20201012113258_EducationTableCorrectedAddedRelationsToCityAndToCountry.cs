using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class EducationTableCorrectedAddedRelationsToCityAndToCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Educations_CityID",
                table: "Educations",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CountryID",
                table: "Educations",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Cities_CityID",
                table: "Educations",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Countries_CountryID",
                table: "Educations",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Cities_CityID",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Countries_CountryID",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CityID",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CountryID",
                table: "Educations");
        }
    }
}
