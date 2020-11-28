using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class addExpertEducationTable14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertEducations",
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
                    University = table.Column<string>(maxLength: 256, nullable: true),
                    Faculty = table.Column<string>(maxLength: 256, nullable: true),
                    GraduationYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertEducations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExpertEducations_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertEducations_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpertEducations_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEducations_CountryID",
                table: "ExpertEducations",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEducations_CreatedBy",
                table: "ExpertEducations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEducations_ModifiedBy",
                table: "ExpertEducations",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertEducations");
        }
    }
}
