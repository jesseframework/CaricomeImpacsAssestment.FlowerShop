using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class CookieManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderDetailTemp_AppOrderHeaders_OrderHeaderId",
                table: "AppOrderDetailTemp");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderDetailTemp_OrderHeaderId",
                table: "AppOrderDetailTemp");

            migrationBuilder.DropColumn(
                name: "CustomerAccountId",
                table: "AppOrderDetailTemp");

            migrationBuilder.DropColumn(
                name: "InvoiceDecription",
                table: "AppOrderDetailTemp");

            migrationBuilder.DropColumn(
                name: "InvoiceDecription",
                table: "AppOrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderHeaderId",
                table: "AppOrderDetailTemp",
                newName: "CookieTrackerId");

            migrationBuilder.AddColumn<Guid>(
                name: "CookieTrackerId",
                table: "AppOrderDetails",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppCustomerAccounts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "CookieTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domain = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiry = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsSecure = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsHttpOnly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
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
                    table.PrimaryKey("PK_CookieTrackers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookieTrackers");

            migrationBuilder.DropColumn(
                name: "CookieTrackerId",
                table: "AppOrderDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppCustomerAccounts");

            migrationBuilder.RenameColumn(
                name: "CookieTrackerId",
                table: "AppOrderDetailTemp",
                newName: "OrderHeaderId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerAccountId",
                table: "AppOrderDetailTemp",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceDecription",
                table: "AppOrderDetailTemp",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceDecription",
                table: "AppOrderDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderDetailTemp_OrderHeaderId",
                table: "AppOrderDetailTemp",
                column: "OrderHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderDetailTemp_AppOrderHeaders_OrderHeaderId",
                table: "AppOrderDetailTemp",
                column: "OrderHeaderId",
                principalTable: "AppOrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
