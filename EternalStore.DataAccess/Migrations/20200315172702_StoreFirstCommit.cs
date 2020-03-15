using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations
{
    public partial class StoreFirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories_tb",
                columns: table => new
                {
                    idCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    isEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_tb", x => x.idCategory);
                });

            migrationBuilder.CreateTable(
                name: "orders_tb",
                columns: table => new
                {
                    idOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    customerName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerNumber = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    customerAddress = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    additionalInformation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    isDelivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_tb", x => x.idOrder);
                });

            migrationBuilder.CreateTable(
                name: "products_tb",
                columns: table => new
                {
                    idProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    price = table.Column<decimal>(type: "decimal", nullable: false),
                    idCategory = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_tb", x => x.idProduct);
                    table.ForeignKey(
                        name: "FK_products_tb_categories_tb_idCategory",
                        column: x => x.idCategory,
                        principalTable: "categories_tb",
                        principalColumn: "idCategory",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderItems_tb",
                columns: table => new
                {
                    idOrderItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProduct = table.Column<int>(nullable: true),
                    qty = table.Column<int>(type: "int", nullable: false),
                    idOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems_tb", x => x.idOrderItem);
                    table.ForeignKey(
                        name: "FK_orderItems_tb_orders_tb_idOrder",
                        column: x => x.idOrder,
                        principalTable: "orders_tb",
                        principalColumn: "idOrder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderItems_tb_products_tb_idProduct",
                        column: x => x.idProduct,
                        principalTable: "products_tb",
                        principalColumn: "idProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_tb_idOrder",
                table: "orderItems_tb",
                column: "idOrder");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_tb_idProduct",
                table: "orderItems_tb",
                column: "idProduct");

            migrationBuilder.CreateIndex(
                name: "IX_products_tb_idCategory",
                table: "products_tb",
                column: "idCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems_tb");

            migrationBuilder.DropTable(
                name: "orders_tb");

            migrationBuilder.DropTable(
                name: "products_tb");

            migrationBuilder.DropTable(
                name: "categories_tb");
        }
    }
}
