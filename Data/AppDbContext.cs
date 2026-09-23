using Microsoft.EntityFrameworkCore;
using ToolTrack.Model;

namespace ToolTrack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tool> Tools { get; set; }
    }
}
