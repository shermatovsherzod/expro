﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expro.Data.Migrations
{
    public partial class WithdrawRequestAndWithdrawRequestStatus_tablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WithdrawRequestStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawRequestStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawRequests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WithdrawRequests_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawRequests_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WithdrawRequests_WithdrawRequestStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "WithdrawRequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRequests_CreatedBy",
                table: "WithdrawRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRequests_ModifiedBy",
                table: "WithdrawRequests",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRequests_StatusID",
                table: "WithdrawRequests",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WithdrawRequests");

            migrationBuilder.DropTable(
                name: "WithdrawRequestStatuses");
        }
    }
}
