using DriverServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<GarageEmployee> GarageEmployees { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Model> Models { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<GarageEmployee>()
                .HasKey(ge => new { ge.GarageId, ge.EmployeeId });

            modelBuilder.Entity<GarageEmployee>()
                .HasOne(ge => ge.Garage)
                .WithMany(g => g.GarageEmployees)
                .HasForeignKey(ge => ge.GarageId);

            modelBuilder.Entity<GarageEmployee>()
                .HasOne(ge => ge.Employee)
                .WithMany(e => e.GarageEmployees)
                .HasForeignKey(ge => ge.EmployeeId);
        }
    }
}
