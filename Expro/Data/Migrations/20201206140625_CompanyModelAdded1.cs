using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class CompanyModelAdded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "LawAreas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 256, nullable: false),
                    LogoID = table.Column<int>(nullable: true),
                    RegionID = table.Column<int>(nullable: true),
                    CityID = table.Column<int>(nullable: true),
                    CityOther = table.Column<string>(maxLength: 256, nullable: true),
                    CompanyDescription = table.Column<string>(maxLength: 4000, nullable: true),
                    CompanyStatusID = table.Column<int>(nullable: false),
                    DateSubmittedForApproval = table.Column<DateTime>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    RejectedReasonText = table.Column<string>(maxLength: 256, nullable: true),
                    WebSite = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyStatuses_CompanyStatusID",
                        column: x => x.CompanyStatusID,
                        principalTable: "CompanyStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Attachments_LogoID",
                        column: x => x.LogoID,
                        principalTable: "Attachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_CompanyID",
                table: "LawAreas",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityID",
                table: "Companies",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyStatusID",
                table: "Companies",
                column: "CompanyStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedBy",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoID",
                table: "Companies",
                column: "LogoID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ModifiedBy",
                table: "Companies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_RegionID",
                table: "Companies",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_Companies_CompanyID",
                table: "LawAreas",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_Companies_CompanyID",
                table: "LawAreas");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_CompanyID",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "LawAreas");
        }
    }
}
