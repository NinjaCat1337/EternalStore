using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations.NotificationDb
{
    public partial class NotificationFirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "schedulers_tb",
                columns: table => new
                {
                    idScheduler = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    executionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedulers_tb", x => x.idScheduler);
                });

            migrationBuilder.CreateTable(
                name: "schedulerMessages_tb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idScheduler = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedulerMessages_tb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedulerMessages_tb_schedulers_tb_idScheduler",
                        column: x => x.idScheduler,
                        principalTable: "schedulers_tb",
                        principalColumn: "idScheduler",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedulerSettings_tb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    executionFrequency = table.Column<int>(type: "int", nullable: false),
                    executionHours = table.Column<int>(type: "int", nullable: false),
                    executionMinutes = table.Column<int>(type: "int", nullable: false),
                    executionDayOfWeek = table.Column<int>(type: "int", nullable: true),
                    idScheduler = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedulerSettings_tb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedulerSettings_tb_schedulers_tb_idScheduler",
                        column: x => x.idScheduler,
                        principalTable: "schedulers_tb",
                        principalColumn: "idScheduler",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emailMessages_tb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    senderEmail = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    recipientEmail = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    idSchedulerMessage = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailMessages_tb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emailMessages_tb_schedulerMessages_tb_idSchedulerMessage",
                        column: x => x.idSchedulerMessage,
                        principalTable: "schedulerMessages_tb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emailMessages_tb_idSchedulerMessage",
                table: "emailMessages_tb",
                column: "idSchedulerMessage");

            migrationBuilder.CreateIndex(
                name: "IX_schedulerMessages_tb_idScheduler",
                table: "schedulerMessages_tb",
                column: "idScheduler",
                unique: true,
                filter: "[idScheduler] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_schedulerSettings_tb_idScheduler",
                table: "schedulerSettings_tb",
                column: "idScheduler",
                unique: true,
                filter: "[idScheduler] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailMessages_tb");

            migrationBuilder.DropTable(
                name: "schedulerSettings_tb");

            migrationBuilder.DropTable(
                name: "schedulerMessages_tb");

            migrationBuilder.DropTable(
                name: "schedulers_tb");
        }
    }
}
