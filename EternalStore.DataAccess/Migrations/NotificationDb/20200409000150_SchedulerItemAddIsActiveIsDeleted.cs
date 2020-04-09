using Microsoft.EntityFrameworkCore.Migrations;

namespace EternalStore.DataAccess.Migrations.NotificationDb
{
    public partial class SchedulerItemAddIsActiveIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "schedulerItems_tb",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "schedulerItems_tb",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "schedulerItems_tb");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "schedulerItems_tb");
        }
    }
}
