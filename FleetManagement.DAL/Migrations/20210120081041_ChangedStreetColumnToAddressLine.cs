using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetManagement.DAL.Migrations
{
    public partial class ChangedStreetColumnToAddressLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Persons",
                newName: "AddressLine");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "InsuranceCompanies",
                newName: "AddressLine");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Garages",
                newName: "AddressLine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "Persons",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "InsuranceCompanies",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "Garages",
                newName: "Street");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Persons",
                type: "int",
                nullable: true);
        }
    }
}
