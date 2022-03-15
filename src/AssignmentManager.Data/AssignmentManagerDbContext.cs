using AssignmentManager.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Data
{
    public class AssignmentManagerDbContext : DbContext
    {
        public AssignmentManagerDbContext()
        {

        }

        public AssignmentManagerDbContext(DbContextOptions<AssignmentManagerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Color> Colors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=AssignmentManager;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
