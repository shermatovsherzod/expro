using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class Attachment_addedDisplayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "Attachments",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Attachments",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachments",
                newName: "Filename");
        }
    }
}
