using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyStockDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EasyStock");

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "EasyStock",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "char(1)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "EasyStock",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                schema: "EasyStock",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                schema: "EasyStock",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "EasyStock",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "EasyStock",
                        principalTable: "Contacts",
                        principalColumn: "ContactId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "EasyStock",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProductImage = table.Column<string>(type: "varchar(200)", nullable: true),
                    ProductDescription = table.Column<string>(type: "varchar(200)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "EasyStock",
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductUnits_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "EasyStock",
                        principalTable: "ProductUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransfers",
                schema: "EasyStock",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StoreIdFrom = table.Column<int>(type: "int", nullable: false),
                    StoreIdTo = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "Decimal(18,2)", precision: 9, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransfers", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_ProductTransfers_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "EasyStock",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                schema: "EasyStock",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<decimal>(type: "Decimal(18,2)", precision: 9, scale: 2, nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => new { x.ProductId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "EasyStock",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "EasyStock",
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLines",
                schema: "EasyStock",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "char(2)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "Decimal(18,2)", precision: 9, scale: 2, nullable: false),
                    Price = table.Column<decimal>(type: "Decimal(18,2)", precision: 9, scale: 2, nullable: false),
                    DataKey = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLines", x => new { x.ProductId, x.TransactionId });
                    table.ForeignKey(
                        name: "FK_TransactionLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "EasyStock",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionLines_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "EasyStock",
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionLines_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "EasyStock",
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_DataKey",
                schema: "EasyStock",
                table: "Contacts",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name",
                schema: "EasyStock",
                table: "Contacts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Category",
                schema: "EasyStock",
                table: "ProductCategories",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_DataKey",
                schema: "EasyStock",
                table: "ProductCategories",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "EasyStock",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DataKey",
                schema: "EasyStock",
                table: "Products",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                schema: "EasyStock",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfers_DataKey",
                schema: "EasyStock",
                table: "ProductTransfers",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfers_ProductId",
                schema: "EasyStock",
                table: "ProductTransfers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_DataKey",
                schema: "EasyStock",
                table: "ProductUnits",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_Unit",
                schema: "EasyStock",
                table: "ProductUnits",
                column: "Unit");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_DataKey",
                schema: "EasyStock",
                table: "stocks",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_StoreId",
                schema: "EasyStock",
                table: "stocks",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DataKey",
                schema: "EasyStock",
                table: "Stores",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_DataKey",
                schema: "EasyStock",
                table: "TransactionLines",
                column: "DataKey");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_StoreId",
                schema: "EasyStock",
                table: "TransactionLines",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_TransactionId",
                schema: "EasyStock",
                table: "TransactionLines",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContactId",
                schema: "EasyStock",
                table: "Transactions",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DataKey",
                schema: "EasyStock",
                table: "Transactions",
                column: "DataKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTransfers",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "stocks",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "TransactionLines",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "Stores",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "ProductUnits",
                schema: "EasyStock");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "EasyStock");
        }
    }
}
