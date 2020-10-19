using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentLawAreaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentLawArea",
                columns: table => new
                {
                    DocumentID = table.Column<int>(nullable: false),
                    LawAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentLawArea", x => new { x.DocumentID, x.LawAreaID });
                    table.ForeignKey(
                        name: "FK_DocumentLawArea_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentLawArea_LawAreas_LawAreaID",
                        column: x => x.LawAreaID,
                        principalTable: "LawAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLawArea_LawAreaID",
                table: "DocumentLawArea",
                column: "LawAreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentLawArea");
        }
    }
}
