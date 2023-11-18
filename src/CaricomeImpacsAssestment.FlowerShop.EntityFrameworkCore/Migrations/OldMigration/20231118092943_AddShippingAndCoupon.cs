using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingAndCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                table: "AppOrderHeaders",
                newName: "Shipping");

            migrationBuilder.AddColumn<double>(
                name: "ShippingCost",
                table: "ItemPrices",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AppOrderPayments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CookieTrackerId",
                table: "AppOrderHeaders",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AppOrderHeaders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Shipping",
                table: "AppOrderDetailTemp",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Shipping",
                table: "AppOrderDetails",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppOrderPayments");

            migrationBuilder.DropColumn(
                name: "CookieTrackerId",
                table: "AppOrderHeaders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppOrderHeaders");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "AppOrderDetailTemp");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "AppOrderDetails");

            migrationBuilder.RenameColumn(
                name: "Shipping",
                table: "AppOrderHeaders",
                newName: "TaxTotal");
        }
    }
}
