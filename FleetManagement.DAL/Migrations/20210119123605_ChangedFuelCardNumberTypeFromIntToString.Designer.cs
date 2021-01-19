﻿// <auto-generated />
using System;
using FleetManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FleetManagement.DAL.Migrations
{
    [DbContext(typeof(FleetManagementContext))]
    [Migration("20210119123605_ChangedFuelCardNumberTypeFromIntToString")]
    partial class ChangedFuelCardNumberTypeFromIntToString
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("FleetManagement.Models.DriverLicense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Categories")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NameHolderFirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NameHolderLastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("DriverLicenses");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AuthenticationType")
                        .HasColumnType("int");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Issued")
                        .HasColumnType("bit");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<int>("PropulsionTypes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FuelCards");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCardOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("FuelCardOptions");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCardWorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HandlerId")
                        .HasColumnType("int");

                    b.Property<int?>("RequesterId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.HasIndex("RequesterId");

                    b.HasIndex("SubjectId");

                    b.ToTable("FuelCardWorkOrder");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FuelCardWorkOrder");
                });

            modelBuilder.Entity("FleetManagement.Models.Garage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Garages");
                });

            modelBuilder.Entity("FleetManagement.Models.InsuranceCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ReferenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InsuranceCompanies");
                });

            modelBuilder.Entity("FleetManagement.Models.LicensePlate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("InUse")
                        .HasColumnType("bit");

                    b.Property<int?>("MotorVehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MotorVehicleId");

                    b.ToTable("LicensePlates");
                });

            modelBuilder.Entity("FleetManagement.Models.LicensePlateSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("InUse")
                        .HasColumnType("bit");

                    b.Property<int>("LicensePlateId")
                        .HasColumnType("int");

                    b.Property<int?>("MotorVehicleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SnapshotDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LicensePlateId");

                    b.HasIndex("MotorVehicleId");

                    b.ToTable("LicensePlateSnapshots");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BodyType")
                        .HasColumnType("int");

                    b.Property<string>("ChassisNumber")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nchar(17)")
                        .IsFixedLength(true);

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<bool>("Operational")
                        .HasColumnType("bit");

                    b.Property<int>("PropulsionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.ToTable("MotorVehicles");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleAccidentInsuranceClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("InsuranceCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LossDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MotorVehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceCompanyId");

                    b.HasIndex("MotorVehicleId");

                    b.ToTable("MotorVehicleAccidentInsuranceClaims");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMaintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DescriptionOfWork")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("GarageId")
                        .HasColumnType("int");

                    b.Property<int?>("MotorVehicleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GarageId");

                    b.HasIndex("MotorVehicleId");

                    b.ToTable("MotorVehicleMaintenances");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMileageSnapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<int>("MotorVehicleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SnapshotDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MotorVehicleId");

                    b.ToTable("MotorVehicleMileageSnapshots");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleWorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HandlerId")
                        .HasColumnType("int");

                    b.Property<int?>("RequesterId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.HasIndex("RequesterId");

                    b.HasIndex("SubjectId");

                    b.ToTable("MotorVehicleWorkOrder");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MotorVehicleWorkOrder");
                });

            modelBuilder.Entity("FleetManagement.Models.PaperDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("WorkOrderStepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkOrderStepId");

                    b.ToTable("PaperDocuments");
                });

            modelBuilder.Entity("FleetManagement.Models.PaperDocumentBinaries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Binaries")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PaperDocumendId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaperDocumendId")
                        .IsUnique();

                    b.ToTable("PaperDocumentBinaries");
                });

            modelBuilder.Entity("FleetManagement.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NationalNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .IsFixedLength(true);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("FleetManagement.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("MotorVehicleAccidentInsuranceClaimId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int?>("WorkOrderStepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MotorVehicleAccidentInsuranceClaimId");

                    b.HasIndex("WorkOrderStepId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("FleetManagement.Models.WorkOrderStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("FuelCardWorkOrderId")
                        .HasColumnType("int");

                    b.Property<int?>("MotorVehicleWorkOrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FuelCardWorkOrderId");

                    b.HasIndex("MotorVehicleWorkOrderId");

                    b.ToTable("WorkOrderStep");
                });

            modelBuilder.Entity("FuelCardFuelCardOption", b =>
                {
                    b.Property<int>("FuelCardsId")
                        .HasColumnType("int");

                    b.Property<int>("OptionsId")
                        .HasColumnType("int");

                    b.HasKey("FuelCardsId", "OptionsId");

                    b.HasIndex("OptionsId");

                    b.ToTable("FuelCardFuelCardOption");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCardRequestWorkOrder", b =>
                {
                    b.HasBaseType("FleetManagement.Models.FuelCardWorkOrder");

                    b.HasDiscriminator().HasValue("FuelCardRequestWorkOrder");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleAccidentWorkOrder", b =>
                {
                    b.HasBaseType("FleetManagement.Models.MotorVehicleWorkOrder");

                    b.Property<int?>("AccidentId")
                        .HasColumnType("int");

                    b.HasIndex("AccidentId");

                    b.HasDiscriminator().HasValue("MotorVehicleAccidentWorkOrder");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMaintenanceWorkOrder", b =>
                {
                    b.HasBaseType("FleetManagement.Models.MotorVehicleWorkOrder");

                    b.Property<int?>("MaintenanceId")
                        .HasColumnType("int");

                    b.HasIndex("MaintenanceId");

                    b.HasDiscriminator().HasValue("MotorVehicleMaintenanceWorkOrder");
                });

            modelBuilder.Entity("FleetManagement.Models.Driver", b =>
                {
                    b.HasBaseType("FleetManagement.Models.Person");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("DriverLicenseId")
                        .HasColumnType("int");

                    b.Property<int?>("FuelCardId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasIndex("DriverLicenseId");

                    b.HasIndex("FuelCardId");

                    b.HasDiscriminator().HasValue("Driver");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCardWorkOrder", b =>
                {
                    b.HasOne("FleetManagement.Models.Person", "Handler")
                        .WithMany()
                        .HasForeignKey("HandlerId");

                    b.HasOne("FleetManagement.Models.Person", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId");

                    b.HasOne("FleetManagement.Models.FuelCard", "Subject")
                        .WithMany("WorkOrders")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handler");

                    b.Navigation("Requester");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("FleetManagement.Models.LicensePlate", b =>
                {
                    b.HasOne("FleetManagement.Models.MotorVehicle", null)
                        .WithMany("LicensePlates")
                        .HasForeignKey("MotorVehicleId");
                });

            modelBuilder.Entity("FleetManagement.Models.LicensePlateSnapshot", b =>
                {
                    b.HasOne("FleetManagement.Models.LicensePlate", "LicensePlate")
                        .WithMany("History")
                        .HasForeignKey("LicensePlateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetManagement.Models.MotorVehicle", "MotorVehicle")
                        .WithMany()
                        .HasForeignKey("MotorVehicleId");

                    b.Navigation("LicensePlate");

                    b.Navigation("MotorVehicle");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicle", b =>
                {
                    b.HasOne("FleetManagement.Models.Driver", "Driver")
                        .WithOne("MotorVehicle")
                        .HasForeignKey("FleetManagement.Models.MotorVehicle", "DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleAccidentInsuranceClaim", b =>
                {
                    b.HasOne("FleetManagement.Models.InsuranceCompany", "InsuranceCompany")
                        .WithMany()
                        .HasForeignKey("InsuranceCompanyId");

                    b.HasOne("FleetManagement.Models.MotorVehicle", "MotorVehicle")
                        .WithMany()
                        .HasForeignKey("MotorVehicleId");

                    b.Navigation("InsuranceCompany");

                    b.Navigation("MotorVehicle");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMaintenance", b =>
                {
                    b.HasOne("FleetManagement.Models.Garage", "Garage")
                        .WithMany()
                        .HasForeignKey("GarageId");

                    b.HasOne("FleetManagement.Models.MotorVehicle", "MotorVehicle")
                        .WithMany()
                        .HasForeignKey("MotorVehicleId");

                    b.Navigation("Garage");

                    b.Navigation("MotorVehicle");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMileageSnapshot", b =>
                {
                    b.HasOne("FleetManagement.Models.MotorVehicle", "MotorVehicle")
                        .WithMany("MileageHistory")
                        .HasForeignKey("MotorVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MotorVehicle");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleWorkOrder", b =>
                {
                    b.HasOne("FleetManagement.Models.Person", "Handler")
                        .WithMany()
                        .HasForeignKey("HandlerId");

                    b.HasOne("FleetManagement.Models.Person", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId");

                    b.HasOne("FleetManagement.Models.MotorVehicle", "Subject")
                        .WithMany("Condition")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handler");

                    b.Navigation("Requester");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("FleetManagement.Models.PaperDocument", b =>
                {
                    b.HasOne("FleetManagement.Models.WorkOrderStep", null)
                        .WithMany("Files")
                        .HasForeignKey("WorkOrderStepId");
                });

            modelBuilder.Entity("FleetManagement.Models.PaperDocumentBinaries", b =>
                {
                    b.HasOne("FleetManagement.Models.PaperDocument", "PaperDocument")
                        .WithOne("Binaries")
                        .HasForeignKey("FleetManagement.Models.PaperDocumentBinaries", "PaperDocumendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaperDocument");
                });

            modelBuilder.Entity("FleetManagement.Models.Photo", b =>
                {
                    b.HasOne("FleetManagement.Models.MotorVehicleAccidentInsuranceClaim", null)
                        .WithMany("DamagePhotos")
                        .HasForeignKey("MotorVehicleAccidentInsuranceClaimId");

                    b.HasOne("FleetManagement.Models.WorkOrderStep", null)
                        .WithMany("Photos")
                        .HasForeignKey("WorkOrderStepId");
                });

            modelBuilder.Entity("FleetManagement.Models.WorkOrderStep", b =>
                {
                    b.HasOne("FleetManagement.Models.FuelCardWorkOrder", null)
                        .WithMany("Steps")
                        .HasForeignKey("FuelCardWorkOrderId");

                    b.HasOne("FleetManagement.Models.MotorVehicleWorkOrder", null)
                        .WithMany("Steps")
                        .HasForeignKey("MotorVehicleWorkOrderId");
                });

            modelBuilder.Entity("FuelCardFuelCardOption", b =>
                {
                    b.HasOne("FleetManagement.Models.FuelCard", null)
                        .WithMany()
                        .HasForeignKey("FuelCardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetManagement.Models.FuelCardOption", null)
                        .WithMany()
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleAccidentWorkOrder", b =>
                {
                    b.HasOne("FleetManagement.Models.MotorVehicleAccidentInsuranceClaim", "Accident")
                        .WithMany()
                        .HasForeignKey("AccidentId");

                    b.Navigation("Accident");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleMaintenanceWorkOrder", b =>
                {
                    b.HasOne("FleetManagement.Models.MotorVehicleMaintenance", "Maintenance")
                        .WithMany()
                        .HasForeignKey("MaintenanceId");

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("FleetManagement.Models.Driver", b =>
                {
                    b.HasOne("FleetManagement.Models.DriverLicense", "DriverLicense")
                        .WithMany()
                        .HasForeignKey("DriverLicenseId");

                    b.HasOne("FleetManagement.Models.FuelCard", "FuelCard")
                        .WithMany("Drivers")
                        .HasForeignKey("FuelCardId");

                    b.Navigation("DriverLicense");

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCard", b =>
                {
                    b.Navigation("Drivers");

                    b.Navigation("WorkOrders");
                });

            modelBuilder.Entity("FleetManagement.Models.FuelCardWorkOrder", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("FleetManagement.Models.LicensePlate", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicle", b =>
                {
                    b.Navigation("Condition");

                    b.Navigation("LicensePlates");

                    b.Navigation("MileageHistory");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleAccidentInsuranceClaim", b =>
                {
                    b.Navigation("DamagePhotos");
                });

            modelBuilder.Entity("FleetManagement.Models.MotorVehicleWorkOrder", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("FleetManagement.Models.PaperDocument", b =>
                {
                    b.Navigation("Binaries")
                        .IsRequired();
                });

            modelBuilder.Entity("FleetManagement.Models.WorkOrderStep", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("FleetManagement.Models.Driver", b =>
                {
                    b.Navigation("MotorVehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
