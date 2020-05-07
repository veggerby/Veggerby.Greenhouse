using Microsoft.EntityFrameworkCore;

namespace Veggerby.Greenhouse
{

    public class GreenhouseContext : DbContext
    {
        public GreenhouseContext(DbContextOptions<GreenhouseContext> options)
            : base(options)
        {

        }

        public DbSet<Measurement> Measurements { get; set; }
    }
}
