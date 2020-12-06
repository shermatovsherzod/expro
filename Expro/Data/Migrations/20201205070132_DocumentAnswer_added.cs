using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAnswer_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AttachmentID = table.Column<int>(nullable: true),
                    DocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentAnswers_Attachments_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentAnswers_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentAnswers_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAnswers_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_AttachmentID",
                table: "DocumentAnswers",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_CreatedBy",
                table: "DocumentAnswers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_DocumentID",
                table: "DocumentAnswers",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAnswers_ModifiedBy",
                table: "DocumentAnswers",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAnswers");
        }
    }
}
