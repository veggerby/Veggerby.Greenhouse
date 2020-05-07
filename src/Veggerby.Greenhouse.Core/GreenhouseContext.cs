using Microsoft.EntityFrameworkCore;

namespace Veggerby.Greenhouse.Core
{

    public class GreenhouseContext : DbContext
    {
        public GreenhouseContext(DbContextOptions<GreenhouseContext> options)
            : base(options)
        {

        }

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>()
                .HasKey(x => x.MeasurementId);

            modelBuilder
                .Entity<Measurement>()
                .HasOne(x => x.Device)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.DeviceId);

            modelBuilder.Entity<Device>()
                .HasKey(x => x.DeviceId);

            modelBuilder.SetDateTimeAsUtc();
        }
    }
}
