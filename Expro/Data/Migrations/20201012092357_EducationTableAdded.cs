using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class EducationTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(nullable: false),
                    University = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true),
                    GraduationYear = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Educations_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserID",
                table: "Educations",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");
        }
    }
}
