using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetManagement.DAL.Migrations
{
    public partial class AddedBrandAndModelColumnToMotorVehicleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "MotorVehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "MotorVehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "MotorVehicles");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "MotorVehicles");
        }
    }
}
