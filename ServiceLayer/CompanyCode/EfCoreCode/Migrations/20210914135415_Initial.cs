﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PartnerCode.EfCoreCode.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "partner");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "partner",
                columns: table => new
                {
                    CompanyTenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataKey = table.Column<string>(type: "varchar(12)", nullable: true),
                    AuthPTenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyTenantId);
                });
            /*
                  migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataKey = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                schema: "invoice",
                columns: table => new
                {
                    LineItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberItems = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    DataKey = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.LineItemId);
                    table.ForeignKey(
                        name: "FK_LineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "invoice",
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

             */
       
            migrationBuilder.CreateIndex(
                name: "IX_Companies_DataKey",
                schema: "partner",
                table: "Companies",
                column: "DataKey");

            /*
                 migrationBuilder.CreateIndex(
                name: "IX_Invoices_DataKey",
                schema: "invoice",
                table: "Invoices",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_DataKey",
                schema: "invoice",
                table: "LineItems",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_InvoiceId",
                schema: "invoice",
                table: "LineItems",
                column: "InvoiceId");
             */
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies",
                schema: "partner");
            /*
                        migrationBuilder.DropTable(
                name: "LineItems",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "invoice");
             */
 
        }
    }
}
