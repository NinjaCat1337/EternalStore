using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations.UsersDb
{
    public partial class UsersFirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users_tb",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    registrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tb", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "userAddresses_tb",
                columns: table => new
                {
                    idUserAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAddresses_tb", x => x.idUserAddress);
                    table.ForeignKey(
                        name: "FK_userAddresses_tb_users_tb_idUser",
                        column: x => x.idUser,
                        principalTable: "users_tb",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userNumbers_tb",
                columns: table => new
                {
                    idUserNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userNumbers_tb", x => x.idUserNumber);
                    table.ForeignKey(
                        name: "FK_userNumbers_tb_users_tb_idUser",
                        column: x => x.idUser,
                        principalTable: "users_tb",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersInformation_tb",
                columns: table => new
                {
                    idUserInformation = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersInformation_tb", x => x.idUserInformation);
                    table.ForeignKey(
                        name: "FK_usersInformation_tb_users_tb_idUserInformation",
                        column: x => x.idUserInformation,
                        principalTable: "users_tb",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userAddresses_tb_idUser",
                table: "userAddresses_tb",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_userNumbers_tb_idUser",
                table: "userNumbers_tb",
                column: "idUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAddresses_tb");

            migrationBuilder.DropTable(
                name: "userNumbers_tb");

            migrationBuilder.DropTable(
                name: "usersInformation_tb");

            migrationBuilder.DropTable(
                name: "users_tb");
        }
    }
}
