using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class ApplicationUserAddUserLawAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLawArea",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    LawAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLawArea", x => new { x.UserID, x.LawAreaID });
                    table.ForeignKey(
                        name: "FK_UserLawArea_LawAreas_LawAreaID",
                        column: x => x.LawAreaID,
                        principalTable: "LawAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLawArea_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLawArea_LawAreaID",
                table: "UserLawArea",
                column: "LawAreaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLawArea");
        }
    }
}
