using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class VacationAndResumeTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResumeStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VacancyStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PatronymicName = table.Column<string>(maxLength: 100, nullable: true),
                    Contact = table.Column<string>(maxLength: 400, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    RegionID = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: true),
                    CityOther = table.Column<string>(maxLength: 256, nullable: true),
                    Education = table.Column<string>(maxLength: 256, nullable: true),
                    GraduationDate = table.Column<DateTime>(nullable: true),
                    WorkExperience = table.Column<string>(maxLength: 256, nullable: true),
                    Languages = table.Column<string>(maxLength: 256, nullable: true),
                    OtherInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ResumeStatusID = table.Column<int>(nullable: false),
                    DateSubmittedForApproval = table.Column<DateTime>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    RejectedReasonText = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resumes_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resumes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resumes_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resumes_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resumes_ResumeStatuses_ResumeStatusID",
                        column: x => x.ResumeStatusID,
                        principalTable: "ResumeStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Company = table.Column<string>(maxLength: 400, nullable: false),
                    RegionID = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: true),
                    CityOther = table.Column<string>(maxLength: 256, nullable: true),
                    Position = table.Column<string>(maxLength: 400, nullable: false),
                    Responsibility = table.Column<string>(maxLength: 2000, nullable: true),
                    Requirements = table.Column<string>(maxLength: 2000, nullable: true),
                    Salary = table.Column<string>(maxLength: 256, nullable: true),
                    Contacts = table.Column<string>(maxLength: 1000, nullable: false),
                    VacancyStatusID = table.Column<int>(nullable: false),
                    DateSubmittedForApproval = table.Column<DateTime>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    RejectedReasonText = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vacancies_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancies_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancies_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancies_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancies_VacancyStatuses_VacancyStatusID",
                        column: x => x.VacancyStatusID,
                        principalTable: "VacancyStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CityID",
                table: "Resumes",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CreatedBy",
                table: "Resumes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ModifiedBy",
                table: "Resumes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_RegionID",
                table: "Resumes",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ResumeStatusID",
                table: "Resumes",
                column: "ResumeStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CityID",
                table: "Vacancies",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CreatedBy",
                table: "Vacancies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ModifiedBy",
                table: "Vacancies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_RegionID",
                table: "Vacancies",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_VacancyStatusID",
                table: "Vacancies",
                column: "VacancyStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "ResumeStatuses");

            migrationBuilder.DropTable(
                name: "VacancyStatuses");
        }
    }
}
