﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Veggerby.Greenhouse.Core;

namespace Veggerby.Greenhouse.Core.Migrations
{
    [DbContext(typeof(GreenhouseContext))]
    [Migration("20200514055734_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Device", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Measurement", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FirstTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<double>("MaxValue")
                        .HasColumnType("float");

                    b.Property<double>("MinValue")
                        .HasColumnType("float");

                    b.Property<string>("PropertyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SensorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("SumValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("MeasurementId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("DeviceId", "SensorId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Property", b =>
                {
                    b.Property<string>("PropertyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Decimals")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Tolerance")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyId");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            PropertyId = "temperature",
                            CreatedUtc = new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390),
                            Decimals = 3,
                            Name = "Temperature",
                            Tolerance = 0.14999999999999999,
                            Unit = "°C"
                        },
                        new
                        {
                            PropertyId = "humidity",
                            CreatedUtc = new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390),
                            Decimals = 3,
                            Name = "Relative Humidity",
                            Tolerance = 0.5,
                            Unit = "%"
                        },
                        new
                        {
                            PropertyId = "pressure",
                            CreatedUtc = new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390),
                            Decimals = 1,
                            Name = "Pressure",
                            Tolerance = 1.0,
                            Unit = "mbar"
                        },
                        new
                        {
                            PropertyId = "soil_humidity",
                            CreatedUtc = new DateTime(2020, 5, 14, 5, 57, 34, 596, DateTimeKind.Utc).AddTicks(7390),
                            Decimals = 0,
                            Name = "Soil Humidity",
                            Tolerance = 100.0
                        });
                });

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Sensor", b =>
                {
                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SensorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId", "SensorId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Measurement", b =>
                {
                    b.HasOne("Veggerby.Greenhouse.Core.Device", "Device")
                        .WithMany("Measurements")
                        .HasForeignKey("DeviceId");

                    b.HasOne("Veggerby.Greenhouse.Core.Property", "Property")
                        .WithMany("Measurements")
                        .HasForeignKey("PropertyId");

                    b.HasOne("Veggerby.Greenhouse.Core.Sensor", "Sensor")
                        .WithMany("Measurements")
                        .HasForeignKey("DeviceId", "SensorId");
                });

            modelBuilder.Entity("Veggerby.Greenhouse.Core.Sensor", b =>
                {
                    b.HasOne("Veggerby.Greenhouse.Core.Device", "Device")
                        .WithMany("Sensors")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}