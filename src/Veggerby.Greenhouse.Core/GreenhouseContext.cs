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

            modelBuilder.SetDateTimeAsUtc();

            var now = DateTime.UtcNow;

            modelBuilder.Entity<Property>().HasData(
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
                });
        }
    }
}
