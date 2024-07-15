using Microsoft.EntityFrameworkCore;
using TestEntityFramework.Models;

namespace TestEntityFramework
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<WorkPlace> WorkPlaces { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(w => w.Users)
                .HasForeignKey(u => u.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.WorkPlace)
                .WithMany(w => w.Departments)
                .HasForeignKey(d => d.WorkPlaceId);
             
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=user_db; Username=postgres; Password=Evgenevich()()7");
        }
    }
}
