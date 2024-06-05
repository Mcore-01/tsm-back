using Microsoft.EntityFrameworkCore;
using tsm_back.Models;

namespace tsm_back.Data
{
    public class TSMContext : DbContext
    {
        public TSMContext()
        {
        }
        public TSMContext(bool createdDB)
        {
            if (createdDB)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Mu$@2001;Database=tsm-project")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<LogEntry> Logs { get; set; }
    }
}
