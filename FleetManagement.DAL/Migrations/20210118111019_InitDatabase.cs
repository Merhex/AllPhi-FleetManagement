using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FleetManagement.DAL.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHolderFirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NameHolderLastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Categories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelCardOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelCardOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    Blocked = table.Column<bool>(type: "bit", nullable: false),
                    Issued = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthenticationType = table.Column<int>(type: "int", nullable: false),
                    FuelTypes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelCardFuelCardOption",
                columns: table => new
                {
                    FuelCardsId = table.Column<int>(type: "int", nullable: false),
                    OptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelCardFuelCardOption", x => new { x.FuelCardsId, x.OptionsId });
                    table.ForeignKey(
                        name: "FK_FuelCardFuelCardOption_FuelCardOptions_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "FuelCardOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelCardFuelCardOption_FuelCards_FuelCardsId",
                        column: x => x.FuelCardsId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalNumber = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DriverLicenseId = table.Column<int>(type: "int", nullable: true),
                    FuelCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_DriverLicenses_DriverLicenseId",
                        column: x => x.DriverLicenseId,
                        principalTable: "DriverLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_FuelCards_FuelCardId",
                        column: x => x.FuelCardId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuelCardWorkOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HandlerId = table.Column<int>(type: "int", nullable: true),
                    RequesterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelCardWorkOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelCardWorkOrder_FuelCards_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelCardWorkOrder_Persons_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuelCardWorkOrder_Persons_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChassisNumber = table.Column<string>(type: "nchar(17)", fixedLength: true, maxLength: 17, nullable: false),
                    Operational = table.Column<bool>(type: "bit", nullable: false),
                    BodyType = table.Column<int>(type: "int", nullable: false),
                    PropulsionType = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorVehicles_Persons_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InUse = table.Column<bool>(type: "bit", nullable: false),
                    MotorVehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePlates_MotorVehicles_MotorVehicleId",
                        column: x => x.MotorVehicleId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorVehicleAccidentInsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LossDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    InsuranceCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorVehicleAccidentInsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorVehicleAccidentInsuranceClaims_InsuranceCompanies_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalTable: "InsuranceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorVehicleAccidentInsuranceClaims_MotorVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorVehicleMaintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DescriptionOfWork = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    GarageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorVehicleMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorVehicleMaintenances_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorVehicleMaintenances_MotorVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorVehicleMileageSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorVehicleMileageSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorVehicleMileageSnapshots_MotorVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlateSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InUse = table.Column<bool>(type: "bit", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    LicensePlateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlateSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePlateSnapshots_LicensePlates_LicensePlateId",
                        column: x => x.LicensePlateId,
                        principalTable: "LicensePlates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicensePlateSnapshots_MotorVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MotorVehicleWorkOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentId = table.Column<int>(type: "int", nullable: true),
                    MaintenanceId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HandlerId = table.Column<int>(type: "int", nullable: true),
                    RequesterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorVehicleWorkOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorVehicleWorkOrder_MotorVehicleAccidentInsuranceClaims_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "MotorVehicleAccidentInsuranceClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorVehicleWorkOrder_MotorVehicleMaintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "MotorVehicleMaintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorVehicleWorkOrder_MotorVehicles_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "MotorVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorVehicleWorkOrder_Persons_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorVehicleWorkOrder_Persons_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FuelCardWorkOrderId = table.Column<int>(type: "int", nullable: true),
                    MotorVehicleWorkOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderStep_FuelCardWorkOrder_FuelCardWorkOrderId",
                        column: x => x.FuelCardWorkOrderId,
                        principalTable: "FuelCardWorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderStep_MotorVehicleWorkOrder_MotorVehicleWorkOrderId",
                        column: x => x.MotorVehicleWorkOrderId,
                        principalTable: "MotorVehicleWorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaperDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkOrderStepId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaperDocuments_WorkOrderStep_WorkOrderStepId",
                        column: x => x.WorkOrderStepId,
                        principalTable: "WorkOrderStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MotorVehicleAccidentInsuranceClaimId = table.Column<int>(type: "int", nullable: true),
                    WorkOrderStepId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_MotorVehicleAccidentInsuranceClaims_MotorVehicleAccidentInsuranceClaimId",
                        column: x => x.MotorVehicleAccidentInsuranceClaimId,
                        principalTable: "MotorVehicleAccidentInsuranceClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_WorkOrderStep_WorkOrderStepId",
                        column: x => x.WorkOrderStepId,
                        principalTable: "WorkOrderStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaperDocumentBinaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Binaries = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PaperDocumendId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperDocumentBinaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaperDocumentBinaries_PaperDocuments_PaperDocumendId",
                        column: x => x.PaperDocumendId,
                        principalTable: "PaperDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelCardFuelCardOption_OptionsId",
                table: "FuelCardFuelCardOption",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelCardWorkOrder_HandlerId",
                table: "FuelCardWorkOrder",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelCardWorkOrder_RequesterId",
                table: "FuelCardWorkOrder",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelCardWorkOrder_SubjectId",
                table: "FuelCardWorkOrder",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlates_MotorVehicleId",
                table: "LicensePlates",
                column: "MotorVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateSnapshots_LicensePlateId",
                table: "LicensePlateSnapshots",
                column: "LicensePlateId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlateSnapshots_VehicleId",
                table: "LicensePlateSnapshots",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleAccidentInsuranceClaims_InsuranceCompanyId",
                table: "MotorVehicleAccidentInsuranceClaims",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleAccidentInsuranceClaims_VehicleId",
                table: "MotorVehicleAccidentInsuranceClaims",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleMaintenances_GarageId",
                table: "MotorVehicleMaintenances",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleMaintenances_VehicleId",
                table: "MotorVehicleMaintenances",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleMileageSnapshots_VehicleId",
                table: "MotorVehicleMileageSnapshots",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicles_DriverId",
                table: "MotorVehicles",
                column: "DriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleWorkOrder_AccidentId",
                table: "MotorVehicleWorkOrder",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleWorkOrder_HandlerId",
                table: "MotorVehicleWorkOrder",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleWorkOrder_MaintenanceId",
                table: "MotorVehicleWorkOrder",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleWorkOrder_RequesterId",
                table: "MotorVehicleWorkOrder",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorVehicleWorkOrder_SubjectId",
                table: "MotorVehicleWorkOrder",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PaperDocumentBinaries_PaperDocumendId",
                table: "PaperDocumentBinaries",
                column: "PaperDocumendId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaperDocuments_WorkOrderStepId",
                table: "PaperDocuments",
                column: "WorkOrderStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DriverLicenseId",
                table: "Persons",
                column: "DriverLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FuelCardId",
                table: "Persons",
                column: "FuelCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MotorVehicleAccidentInsuranceClaimId",
                table: "Photos",
                column: "MotorVehicleAccidentInsuranceClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_WorkOrderStepId",
                table: "Photos",
                column: "WorkOrderStepId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderStep_FuelCardWorkOrderId",
                table: "WorkOrderStep",
                column: "FuelCardWorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderStep_MotorVehicleWorkOrderId",
                table: "WorkOrderStep",
                column: "MotorVehicleWorkOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelCardFuelCardOption");

            migrationBuilder.DropTable(
                name: "LicensePlateSnapshots");

            migrationBuilder.DropTable(
                name: "MotorVehicleMileageSnapshots");

            migrationBuilder.DropTable(
                name: "PaperDocumentBinaries");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "FuelCardOptions");

            migrationBuilder.DropTable(
                name: "LicensePlates");

            migrationBuilder.DropTable(
                name: "PaperDocuments");

            migrationBuilder.DropTable(
                name: "WorkOrderStep");

            migrationBuilder.DropTable(
                name: "FuelCardWorkOrder");

            migrationBuilder.DropTable(
                name: "MotorVehicleWorkOrder");

            migrationBuilder.DropTable(
                name: "MotorVehicleAccidentInsuranceClaims");

            migrationBuilder.DropTable(
                name: "MotorVehicleMaintenances");

            migrationBuilder.DropTable(
                name: "InsuranceCompanies");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropTable(
                name: "MotorVehicles");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "DriverLicenses");

            migrationBuilder.DropTable(
                name: "FuelCards");
        }
    }
}
