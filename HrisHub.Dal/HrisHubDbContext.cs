using Microsoft.EntityFrameworkCore;
using HrisHub.Models;

namespace HrisHub.Dal
{
    public class HrisHubDbContext: DbContext
    {
        public HrisHubDbContext()
        {
        
        }

        public HrisHubDbContext(DbContextOptions options): base(options)
        {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured)
            {
                return;
            }

            options.UseSqlServer("Data Source=DELL-CEBUHQ-001\\SQLEXPRESS;Initial Catalog=HrisHubDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "John Doe", Address = "1h Street", City = "Flower Capital", Country = "Wano", ZipCode = "9160", Email = "john.doe@mailinator.com", ImagePath = "https://allworldpm.com/wp-content/uploads/2016/10/230x230-avatar-dummy-profile-pic.jpg", 
                 Phone = "0901111111", Skillsets = "Fullstack dev"}
            );

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "Employee", Description = "Employee" },
                new Role() { Id = 2, Name = "HR", Description = "HR" }
            );
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Event> Events { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}