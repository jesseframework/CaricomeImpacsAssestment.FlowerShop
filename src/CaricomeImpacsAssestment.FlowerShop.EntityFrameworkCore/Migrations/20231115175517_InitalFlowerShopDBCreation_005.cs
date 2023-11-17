using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class InitalFlowerShopDBCreation005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppOrderDetailTemp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerAccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TaxID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductGroupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Decription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceDecription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExchangeRate = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    UnitPrice = table.Column<double>(type: "double", nullable: false),
                    ListPrice = table.Column<double>(type: "double", nullable: false),
                    TaxAmount = table.Column<double>(type: "double", nullable: false),
                    LineDiscount = table.Column<double>(type: "double", nullable: false),
                    SubTotal = table.Column<double>(type: "double", nullable: false),
                    LineTotal = table.Column<double>(type: "double", nullable: false),
                    OrderHeaderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrderDetailTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrderDetailTemp_AppCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderDetailTemp_AppItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "AppItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderDetailTemp_AppOrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "AppOrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrderDetailTemp_AppProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "AppProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderDetailTemp_CategoryId",
                table: "AppOrderDetailTemp",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderDetailTemp_ItemId",
                table: "AppOrderDetailTemp",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderDetailTemp_OrderHeaderId",
                table: "AppOrderDetailTemp",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderDetailTemp_ProductGroupId",
                table: "AppOrderDetailTemp",
                column: "ProductGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOrderDetailTemp");
        }
    }
}
