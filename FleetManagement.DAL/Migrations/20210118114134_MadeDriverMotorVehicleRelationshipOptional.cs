using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetManagement.DAL.Migrations
{
    public partial class MadeDriverMotorVehicleRelationshipOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicensePlateSnapshots_MotorVehicles_VehicleId",
                table: "LicensePlateSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleAccidentInsuranceClaims_MotorVehicles_VehicleId",
                table: "MotorVehicleAccidentInsuranceClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleMaintenances_MotorVehicles_VehicleId",
                table: "MotorVehicleMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleMileageSnapshots_MotorVehicles_VehicleId",
                table: "MotorVehicleMileageSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicles_Persons_DriverId",
                table: "MotorVehicles");

            migrationBuilder.DropIndex(
                name: "IX_MotorVehicles_DriverId",
                table: "MotorVehicles");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "MotorVehicleMileageSnapshots",
                newName: "MotorVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleMileageSnapshots_VehicleId",
                table: "MotorVehicleMileageSnapshots",
                newName: "IX_MotorVehicleMileageSnapshots_MotorVehicleId");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "MotorVehicleMaintenances",
                newName: "MotorVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleMaintenances_VehicleId",
                table: "MotorVehicleMaintenances",
                newName: "IX_MotorVehicleMaintenances_MotorVehicleId");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                newName: "MotorVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleAccidentInsuranceClaims_VehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                newName: "IX_MotorVehicleAccidentInsuranceClaims_MotorVehicleId");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "LicensePlateSnapshots",
                newName: "MotorVehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_LicensePlateSnapshots_VehicleId",
                table: "LicensePlateSnapshots",
                newName: "IX_LicensePlateSnapshots_MotorVehicleId");

            migrationBuilder.RenameColumn(
                name: "FuelTypes",
                table: "FuelCards",
                newName: "PropulsionTypes");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "MotorVehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicles_DriverId",
                table: "MotorVehicles",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LicensePlateSnapshots_MotorVehicles_MotorVehicleId",
                table: "LicensePlateSnapshots",
                column: "MotorVehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleAccidentInsuranceClaims_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                column: "MotorVehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleMaintenances_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleMaintenances",
                column: "MotorVehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleMileageSnapshots_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleMileageSnapshots",
                column: "MotorVehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicles_Persons_DriverId",
                table: "MotorVehicles",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LicensePlateSnapshots_MotorVehicles_MotorVehicleId",
                table: "LicensePlateSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleAccidentInsuranceClaims_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleAccidentInsuranceClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleMaintenances_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicleMileageSnapshots_MotorVehicles_MotorVehicleId",
                table: "MotorVehicleMileageSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_MotorVehicles_Persons_DriverId",
                table: "MotorVehicles");

            migrationBuilder.DropIndex(
                name: "IX_MotorVehicles_DriverId",
                table: "MotorVehicles");

            migrationBuilder.RenameColumn(
                name: "MotorVehicleId",
                table: "MotorVehicleMileageSnapshots",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleMileageSnapshots_MotorVehicleId",
                table: "MotorVehicleMileageSnapshots",
                newName: "IX_MotorVehicleMileageSnapshots_VehicleId");

            migrationBuilder.RenameColumn(
                name: "MotorVehicleId",
                table: "MotorVehicleMaintenances",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleMaintenances_MotorVehicleId",
                table: "MotorVehicleMaintenances",
                newName: "IX_MotorVehicleMaintenances_VehicleId");

            migrationBuilder.RenameColumn(
                name: "MotorVehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_MotorVehicleAccidentInsuranceClaims_MotorVehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                newName: "IX_MotorVehicleAccidentInsuranceClaims_VehicleId");

            migrationBuilder.RenameColumn(
                name: "MotorVehicleId",
                table: "LicensePlateSnapshots",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_LicensePlateSnapshots_MotorVehicleId",
                table: "LicensePlateSnapshots",
                newName: "IX_LicensePlateSnapshots_VehicleId");

            migrationBuilder.RenameColumn(
                name: "PropulsionTypes",
                table: "FuelCards",
                newName: "FuelTypes");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "MotorVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicles_DriverId",
                table: "MotorVehicles",
                column: "DriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LicensePlateSnapshots_MotorVehicles_VehicleId",
                table: "LicensePlateSnapshots",
                column: "VehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleAccidentInsuranceClaims_MotorVehicles_VehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                column: "VehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleMaintenances_MotorVehicles_VehicleId",
                table: "MotorVehicleMaintenances",
                column: "VehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicleMileageSnapshots_MotorVehicles_VehicleId",
                table: "MotorVehicleMileageSnapshots",
                column: "VehicleId",
                principalTable: "MotorVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotorVehicles_Persons_DriverId",
                table: "MotorVehicles",
                column: "DriverId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
