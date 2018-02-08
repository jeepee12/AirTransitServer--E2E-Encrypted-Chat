using Microsoft.EntityFrameworkCore;

namespace AirTransitServer.Models
{
    public sealed class RegistryContext : DbContext
    {
        public RegistryContext(DbContextOptions<RegistryContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Registry> Registries { get; set; }
    }
}
