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
        public DbSet<PropertyDomain> PropertyDomains { get; set; }
        public DbSet<PropertyDomainValue> PropertyDomainValues { get; set; }
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

            modelBuilder.Entity<PropertyDomain>()
                .HasKey(x => x.PropertyDomainId);

            modelBuilder.Entity<PropertyDomainValue>()
                .HasKey(x => new { x.PropertyDomainId, x.PropertyDomainValueId } );

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
                .Entity<Property>()
                .HasOne(x => x.PropertyDomain)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.PropertyDomainId);

            modelBuilder
                .Entity<PropertyDomainValue>()
                .HasOne(x => x.PropertyDomain)
                .WithMany(x => x.PropertyDomainValues)
                .HasForeignKey(x => x.PropertyDomainId);

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

            modelBuilder.Entity<PropertyDomain>()
                .HasData(
                    new PropertyDomain
                    {
                        PropertyDomainId = "charging",
                        Name = "Charging Discrete Values",
                        CreatedUtc = now
                    },
                    new PropertyDomain
                    {
                        PropertyDomainId = "power_input",
                        Name = "Power Input Discrete Values",
                        CreatedUtc = now
                    }
                );

            modelBuilder.Entity<PropertyDomainValue>()
                .HasData(
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "not_charging",
                        PropertyDomainId = "charging",
                        Name = "NOT CHARGING",
                        LowerValue = 0,
                        UpperValue = 0,
                        CreatedUtc = now
                    },
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "charging",
                        PropertyDomainId = "charging",
                        Name = "CHARGING",
                        LowerValue = 1,
                        UpperValue = 1,
                        CreatedUtc = now
                    },
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "not_present",
                        PropertyDomainId = "power_input",
                        Name = "NOT_PRESENT",
                        LowerValue = -1,
                        UpperValue = -1,
                        CreatedUtc = now
                    },
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "bad",
                        PropertyDomainId = "power_input",
                        Name = "BAD",
                        LowerValue = 0,
                        UpperValue = 0,
                        CreatedUtc = now
                    },
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "weak",
                        PropertyDomainId = "power_input",
                        Name = "WEAK",
                        LowerValue = 1,
                        UpperValue = 1,
                        CreatedUtc = now
                    },
                    new PropertyDomainValue
                    {
                        PropertyDomainValueId = "present",
                        PropertyDomainId = "power_input",
                        Name = "PRESENT",
                        LowerValue = 2,
                        UpperValue = 2,
                        CreatedUtc = now
                    }
                );

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
                        PropertyId = "battery_current",
                        Name = "Battery Current",
                        Unit = "mA",
                        Tolerance = 1,
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
                    },
                    new Property
                    {
                        PropertyId = "charging",
                        Name = "Is Battery Charging?",
                        Unit = "",
                        Tolerance = 0.01,
                        Decimals = 0,
                        PropertyDomainId = "charging",
                        CreatedUtc = now
                    },
                    new Property
                    {
                        PropertyId = "power_input",
                        Name = "Power Input",
                        Unit = "",
                        Tolerance = 0.01,
                        Decimals = 0,
                        PropertyDomainId = "power_input",
                        CreatedUtc = now
                    }
                );
        }
    }
}
