using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class AddLocalizationToBaseModelDropdownableNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "WithdrawRequestStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "VacancyStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "UserStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "ResumeStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "LawAreas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "Genders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "DocumentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "DocumentStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "CompanyStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameID",
                table: "Cities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRequestStatuses_NameID",
                table: "WithdrawRequestStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyStatuses_NameID",
                table: "VacancyStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatuses_NameID",
                table: "UserStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeStatuses_NameID",
                table: "ResumeStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_NameID",
                table: "Regions",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_NameID",
                table: "LawAreas",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_NameID",
                table: "Languages",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_Genders_NameID",
                table: "Genders",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_NameID",
                table: "DocumentTypes",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentStatuses_NameID",
                table: "DocumentStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_NameID",
                table: "Countries",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStatuses_NameID",
                table: "CompanyStatuses",
                column: "NameID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_NameID",
                table: "Cities",
                column: "NameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_LocalizationShorts_NameID",
                table: "Cities",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStatuses_LocalizationShorts_NameID",
                table: "CompanyStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_LocalizationShorts_NameID",
                table: "Countries",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentStatuses_LocalizationShorts_NameID",
                table: "DocumentStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_LocalizationShorts_NameID",
                table: "DocumentTypes",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_LocalizationShorts_NameID",
                table: "Genders",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LocalizationShorts_NameID",
                table: "Languages",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_LocalizationShorts_NameID",
                table: "LawAreas",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_LocalizationShorts_NameID",
                table: "Regions",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResumeStatuses_LocalizationShorts_NameID",
                table: "ResumeStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyStatuses_LocalizationShorts_NameID",
                table: "VacancyStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WithdrawRequestStatuses_LocalizationShorts_NameID",
                table: "WithdrawRequestStatuses",
                column: "NameID",
                principalTable: "LocalizationShorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_LocalizationShorts_NameID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStatuses_LocalizationShorts_NameID",
                table: "CompanyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_LocalizationShorts_NameID",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentStatuses_LocalizationShorts_NameID",
                table: "DocumentStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_LocalizationShorts_NameID",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Genders_LocalizationShorts_NameID",
                table: "Genders");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LocalizationShorts_NameID",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_LocalizationShorts_NameID",
                table: "LawAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_LocalizationShorts_NameID",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_ResumeStatuses_LocalizationShorts_NameID",
                table: "ResumeStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStatuses_LocalizationShorts_NameID",
                table: "UserStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancyStatuses_LocalizationShorts_NameID",
                table: "VacancyStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_WithdrawRequestStatuses_LocalizationShorts_NameID",
                table: "WithdrawRequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_WithdrawRequestStatuses_NameID",
                table: "WithdrawRequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_VacancyStatuses_NameID",
                table: "VacancyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserStatuses_NameID",
                table: "UserStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ResumeStatuses_NameID",
                table: "ResumeStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Regions_NameID",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_NameID",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_Languages_NameID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Genders_NameID",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_NameID",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentStatuses_NameID",
                table: "DocumentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Countries_NameID",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStatuses_NameID",
                table: "CompanyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Cities_NameID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "WithdrawRequestStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "UserStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "ResumeStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "DocumentStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "CompanyStatuses");

            migrationBuilder.DropColumn(
                name: "NameID",
                table: "Cities");
        }
    }
}
