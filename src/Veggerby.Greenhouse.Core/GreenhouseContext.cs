using System;
using Microsoft.EntityFrameworkCore;

namespace Veggerby.Greenhouse.Core
{

    public class GreenhouseContext : DbContext
    {
        public GreenhouseContext(DbContextOptions<GreenhouseContext> options)
            : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Annotation> Annotations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(x => x.DeviceId);

            modelBuilder.Entity<Sensor>()
                .HasKey(x => new { x.DeviceId, x.SensorId });

            modelBuilder.Entity<Property>()
                .HasKey(x => x.PropertyId);

            modelBuilder.Entity<Measurement>()
                .HasKey(x => x.MeasurementId);

            modelBuilder.Entity<Annotation>()
                .HasKey(x => x.AnnotationId);

            modelBuilder
                .Entity<Measurement>()
                .HasOne(x => x.Device)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.DeviceId);

            modelBuilder
                .Entity<Measurement>()
                .HasOne(x => x.Sensor)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => new { x.DeviceId, x.SensorId });

            modelBuilder
                .Entity<Measurement>()
                .HasOne(x => x.Property)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.PropertyId);

            modelBuilder
                .Entity<Annotation>()
                .HasOne(x => x.Measurement)
                .WithMany(x => x.Annotations)
                .HasForeignKey(x => x.MeasurementId);

            modelBuilder
                .Entity<Device>()
                .Property(x => x.Enabled)
                .HasDefaultValue(true);

            modelBuilder
                .Entity<Sensor>()
                .Property(x => x.Enabled)
                .HasDefaultValue(true);

            modelBuilder.SetDateTimeAsUtc();

            var now = new DateTime(2020, 05, 01, 0, 0,0, DateTimeKind.Utc);

            modelBuilder.Entity<Property>()
                .HasData(
                    new Property
                    {
                        PropertyId = "temperature",
                        Name = "Temperature",
                        Unit = "°C",
                        Tolerance = 0.15,
                        Decimals = 3,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "humidity",
                        Name = "Relative Humidity",
                        Unit = "%",
                        Tolerance = 0.5,
                        Decimals = 3,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "pressure",
                        Name = "Pressure",
                        Unit = "mbar",
                        Tolerance = 1,
                        Decimals = 1,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "soil_humidity",
                        Name = "Soil Humidity",
                        Unit = null,
                        Tolerance = 100,
                        Decimals = 0,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "battery_charge",
                        Name = "Battery Charge",
                        Unit = "%",
                        Tolerance = 0.1,
                        Decimals = 0,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "battery_temperature",
                        Name = "Battery Temperature",
                        Unit = "°C",
                        Tolerance = 0.1,
                        Decimals = 0,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "battery_voltage",
                        Name = "Battery Voltage",
                        Unit = "mV",
                        Tolerance = 5,
                        Decimals = 0,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "io_voltage",
                        Name = "I/O Voltage",
                        Unit = "mV",
                        Tolerance = 10,
                        Decimals = 0,
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "io_current",
                        Name = "I/O Current",
                        Unit = "mA",
                        Tolerance = 1,
                        Decimals = 0,
                        CreatedUtc = now
                    }
                );
        }
    }
}
