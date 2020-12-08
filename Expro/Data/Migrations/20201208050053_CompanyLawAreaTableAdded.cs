using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class CompanyLawAreaTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LawAreas_Companies_CompanyID",
                table: "LawAreas");

            migrationBuilder.DropIndex(
                name: "IX_LawAreas_CompanyID",
                table: "LawAreas");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "LawAreas");

            migrationBuilder.CreateTable(
                name: "CompanyLawArea",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    LawAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLawArea", x => new { x.CompanyID, x.LawAreaID });
                    table.ForeignKey(
                        name: "FK_CompanyLawArea_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyLawArea_LawAreas_LawAreaID",
                        column: x => x.LawAreaID,
                        principalTable: "LawAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLawArea_LawAreaID",
                table: "CompanyLawArea",
                column: "LawAreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLawArea");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "LawAreas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LawAreas_CompanyID",
                table: "LawAreas",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_LawAreas_Companies_CompanyID",
                table: "LawAreas",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
