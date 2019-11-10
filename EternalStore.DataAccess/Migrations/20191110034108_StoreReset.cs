using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations
{
    public partial class StoreReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "categories_tb",
            //    columns: table => new
            //    {
            //        idCategory = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //        isEnabled = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_categories_tb", x => x.idCategory);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "products_tb",
            //    columns: table => new
            //    {
            //        idProduct = table.Column<int>(type: "int", nullable: false),
            //        idCategory = table.Column<int>(type: "int", nullable: false),
            //        name = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //        description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
            //        price = table.Column<decimal>(type: "decimal", nullable: false),
            //        Product_idCategory = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_products_tb", x => new { x.idCategory, x.idProduct });
            //        table.ForeignKey(
            //            name: "FK_products_tb_categories_tb_Product_idCategory",
            //            column: x => x.Product_idCategory,
            //            principalTable: "categories_tb",
            //            principalColumn: "idCategory",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_products_tb_Product_idCategory",
            //    table: "products_tb",
            //    column: "Product_idCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_tb");

            migrationBuilder.DropTable(
                name: "categories_tb");
        }
    }
}
