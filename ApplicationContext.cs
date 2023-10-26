using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
