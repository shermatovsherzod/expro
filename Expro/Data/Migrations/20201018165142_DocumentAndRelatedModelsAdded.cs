using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class DocumentAndRelatedModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DocumentTypeID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 1024, nullable: false),
                    LanguageID = table.Column<int>(nullable: false),
                    AttachmentID = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    DocumentStatusID = table.Column<int>(nullable: false),
                    DateSubmittedForApproval = table.Column<DateTime>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    DateRejected = table.Column<DateTime>(nullable: true),
                    CancellationJobID = table.Column<string>(nullable: true),
                    NumberOfViews = table.Column<int>(nullable: false),
                    NumberOfPurchases = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Documents_Attachments_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentStatuses_DocumentStatusID",
                        column: x => x.DocumentStatusID,
                        principalTable: "DocumentStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AttachmentID",
                table: "Documents",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatedBy",
                table: "Documents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentStatusID",
                table: "Documents",
                column: "DocumentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeID",
                table: "Documents",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LanguageID",
                table: "Documents",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ModifiedBy",
                table: "Documents",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "DocumentStatuses");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
