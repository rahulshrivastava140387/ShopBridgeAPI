using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridgeAPI.Migrations
{
    public partial class ProductAPIContextDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    productDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    productCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    productPrice = table.Column<double>(type: "float", nullable: false),
                    productQuantity = table.Column<double>(type: "float", nullable: false),
                    productImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inStock = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
