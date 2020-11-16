using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class ClickTransactionTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClickTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClickTransID = table.Column<int>(nullable: false),
                    ServiceID = table.Column<int>(nullable: false),
                    ClickPaydocID = table.Column<int>(nullable: false),
                    MerchantTransID = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    Error = table.Column<int>(nullable: false),
                    ErrorNote = table.Column<string>(nullable: true),
                    SignTime = table.Column<string>(nullable: true),
                    SignString = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: false),
                    DateSuccess = table.Column<DateTime>(nullable: true),
                    DateCancelled = table.Column<DateTime>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClickTransactions_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClickTransactions_UserID",
                table: "ClickTransactions",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClickTransactions");
        }
    }
}
