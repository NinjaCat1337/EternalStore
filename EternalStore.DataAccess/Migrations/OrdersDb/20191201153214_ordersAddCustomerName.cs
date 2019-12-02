using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations.OrdersDb
{
    public partial class ordersAddCustomerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_tb_orders_tb_idOrder",
                table: "orderItems_tb");

            migrationBuilder.AddColumn<string>(
                name: "customerName",
                table: "orders_tb",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_tb_orders_tb_idOrder",
                table: "orderItems_tb",
                column: "idOrder",
                principalTable: "orders_tb",
                principalColumn: "idOrder",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_tb_orders_tb_idOrder",
                table: "orderItems_tb");

            migrationBuilder.DropColumn(
                name: "customerName",
                table: "orders_tb");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_tb_orders_tb_idOrder",
                table: "orderItems_tb",
                column: "idOrder",
                principalTable: "orders_tb",
                principalColumn: "idOrder",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
