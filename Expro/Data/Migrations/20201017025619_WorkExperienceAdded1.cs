﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class WorkExperienceAdded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkExperiences",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 256, nullable: true),
                    PlaceOfWork = table.Column<string>(maxLength: 256, nullable: true),
                    Position = table.Column<string>(maxLength: 256, nullable: true),
                    WorkPeriodFrom = table.Column<string>(maxLength: 100, nullable: true),
                    WorkPeriodTo = table.Column<string>(maxLength: 100, nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperiences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_CountryID",
                table: "WorkExperiences",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_CreatedBy",
                table: "WorkExperiences",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_ModifiedBy",
                table: "WorkExperiences",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_UserID",
                table: "WorkExperiences",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkExperiences");
        }
    }
}
