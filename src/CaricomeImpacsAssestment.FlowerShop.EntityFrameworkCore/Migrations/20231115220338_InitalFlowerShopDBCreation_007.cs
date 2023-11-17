using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class InitalFlowerShopDBCreation007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatureProduct",
                table: "AppItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "AppCategories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatureProduct",
                table: "AppItems");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "AppCategories");
        }
    }
}
