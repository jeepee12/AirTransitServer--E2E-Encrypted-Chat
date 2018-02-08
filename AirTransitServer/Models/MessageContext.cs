using Microsoft.EntityFrameworkCore;

namespace AirTransitServer.Models
{
    public sealed class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Message> Messages { get; set; }
    }
}
