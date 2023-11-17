using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class InitalFlowerShopDBCreation001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerNo",
                table: "AppCustomerAccounts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNo",
                table: "AppCustomerAccounts");
        }
    }
}
