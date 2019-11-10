using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations
{
    public partial class StoreCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_products_tb_idProduct",
                table: "products_tb",
                column: "idProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_products_tb_idProduct",
                table: "products_tb");
        }
    }
}
