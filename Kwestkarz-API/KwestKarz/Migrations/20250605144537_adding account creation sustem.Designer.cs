﻿// <auto-generated />
using System;
using System.Collections.Generic;
using KwestKarz.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KwestKarz.Migrations
{
    [DbContext(typeof(KwestKarzDbContext))]
    [Migration("20250605144537_adding account creation sustem")]
    partial class addingaccountcreationsustem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("kwestkarzbusinessdata")
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KwestKarz.Entities.ApiClientClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ApiClientKeyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApiClientKeyId");

                    b.ToTable("ApiClientClaims", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.ApiClientKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("KeyHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUsed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApiClientKeys", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.ContactLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LogId"));

                    b.Property<string>("ContactType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("LogId");

                    b.HasIndex("GuestId");

                    b.ToTable("ContactLogs", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GuestId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("InternalRating")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("boolean");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TuroUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GuestId");

                    b.ToTable("Guests", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.LogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreviousHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LogEntries", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.Memo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.PrimitiveCollection<List<string>>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Memos", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.OutstandingCharge", b =>
                {
                    b.Property<int>("ChargeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ChargeId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("ChargeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateIncurred")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateResolved")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TripId")
                        .HasColumnType("integer");

                    b.HasKey("ChargeId");

                    b.HasIndex("GuestId");

                    b.ToTable("OutstandingCharges", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.TechLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Detail")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("TechLogs", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TripId"));

                    b.Property<bool>("DamageReported")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Earnings")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<bool>("LateReturn")
                        .HasColumnType("boolean");

                    b.Property<int>("MilesDriven")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("TollFlag")
                        .HasColumnType("boolean");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("TripId");

                    b.HasIndex("GuestId");

                    b.ToTable("Trips", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.TripEarnings", b =>
                {
                    b.Property<string>("ReservationID")
                        .HasColumnType("text");

                    b.Property<string>("Guest")
                        .HasColumnType("text");

                    b.Property<DateTime>("ImportedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PickupLocation")
                        .HasColumnType("text");

                    b.Property<string>("ReturnLocation")
                        .HasColumnType("text");

                    b.Property<string>("RowHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalEarnings")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("TripEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TripStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TripStatus")
                        .HasColumnType("text");

                    b.Property<string>("Vehicle")
                        .HasColumnType("text");

                    b.Property<string>("VehicleName")
                        .HasColumnType("text");

                    b.HasKey("ReservationID");

                    b.ToTable("TripEarnings", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("RequiresPasswordReset")
                        .HasColumnType("boolean");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("UserRoles", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LicensePlateNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Package")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PaintCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("character varying(17)");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Vehicles", "kwestkarzbusinessdata");
                });

            modelBuilder.Entity("KwestKarz.Entities.VehicleEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int?>("Odometer")
                        .HasColumnType("integer");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleEvents", "kwestkarzbusinessdata");

                    b.HasDiscriminator<string>("EventType").HasValue("VehicleEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("KwestKarz.Entities.Maintenance.IncidentReport", b =>
                {
                    b.HasBaseType("KwestKarz.Entities.VehicleEvent");

                    b.Property<string>("ClaimId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("ReportedToTuro")
                        .HasColumnType("boolean");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasDiscriminator().HasValue("Incident");
                });

            modelBuilder.Entity("KwestKarz.Entities.Maintenance.InspectionEntry", b =>
                {
                    b.HasBaseType("KwestKarz.Entities.VehicleEvent");

                    b.Property<string>("InspectionType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Inspector")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasDiscriminator().HasValue("Inspection");
                });

            modelBuilder.Entity("KwestKarz.Entities.Maintenance.MaintenanceEntry", b =>
                {
                    b.HasBaseType("KwestKarz.Entities.VehicleEvent");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PerformedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.ToTable("VehicleEvents", "kwestkarzbusinessdata", t =>
                        {
                            t.Property("Cost")
                                .HasColumnName("MaintenanceEntry_Cost");
                        });

                    b.HasDiscriminator().HasValue("Maintenance");
                });

            modelBuilder.Entity("KwestKarz.Entities.Maintenance.RepairEntry", b =>
                {
                    b.HasBaseType("KwestKarz.Entities.VehicleEvent");

                    b.Property<string>("ComponentAffected")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RepairType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasDiscriminator().HasValue("Repair");
                });

            modelBuilder.Entity("KwestKarz.Entities.ApiClientClaim", b =>
                {
                    b.HasOne("KwestKarz.Entities.ApiClientKey", "ApiClientKey")
                        .WithMany("Claims")
                        .HasForeignKey("ApiClientKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApiClientKey");
                });

            modelBuilder.Entity("KwestKarz.Entities.ContactLog", b =>
                {
                    b.HasOne("KwestKarz.Entities.Guest", "Guest")
                        .WithMany("ContactLogs")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("KwestKarz.Entities.OutstandingCharge", b =>
                {
                    b.HasOne("KwestKarz.Entities.Guest", "Guest")
                        .WithMany("OutstandingCharges")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("KwestKarz.Entities.Trip", b =>
                {
                    b.HasOne("KwestKarz.Entities.Guest", "Guest")
                        .WithMany("Trips")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("KwestKarz.Entities.UserRole", b =>
                {
                    b.HasOne("KwestKarz.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwestKarz.Entities.UserAccount", "UserAccount")
                        .WithMany("Roles")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("KwestKarz.Entities.VehicleEvent", b =>
                {
                    b.HasOne("KwestKarz.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("KwestKarz.Entities.ApiClientKey", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("KwestKarz.Entities.Guest", b =>
                {
                    b.Navigation("ContactLogs");

                    b.Navigation("OutstandingCharges");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("KwestKarz.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("KwestKarz.Entities.UserAccount", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
