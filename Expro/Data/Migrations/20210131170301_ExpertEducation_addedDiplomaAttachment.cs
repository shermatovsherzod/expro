using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class ExpertEducation_addedDiplomaAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiplomaID",
                table: "ExpertEducations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertEducations_DiplomaID",
                table: "ExpertEducations",
                column: "DiplomaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertEducations_Attachments_DiplomaID",
                table: "ExpertEducations",
                column: "DiplomaID",
                principalTable: "Attachments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertEducations_Attachments_DiplomaID",
                table: "ExpertEducations");

            migrationBuilder.DropIndex(
                name: "IX_ExpertEducations_DiplomaID",
                table: "ExpertEducations");

            migrationBuilder.DropColumn(
                name: "DiplomaID",
                table: "ExpertEducations");
        }
    }
}
