using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaricomeImpacsAssestment.FlowerShop.Migrations
{
    /// <inheritdoc />
    public partial class InitalFlowerShopDBCreation009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemImagesUrl1",
                table: "AppItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ItemImagesUrl2",
                table: "AppItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ItemImagesUrl3",
                table: "AppItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemImagesUrl1",
                table: "AppItems");

            migrationBuilder.DropColumn(
                name: "ItemImagesUrl2",
                table: "AppItems");

            migrationBuilder.DropColumn(
                name: "ItemImagesUrl3",
                table: "AppItems");
        }
    }
}
