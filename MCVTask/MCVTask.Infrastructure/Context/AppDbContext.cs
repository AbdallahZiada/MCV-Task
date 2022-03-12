using MCVTask.Domain._SharedKernal;
using MCVTask.Domain.Department.Entity;
using MCVTask.Domain.Employee.Entity;
using Microsoft.EntityFrameworkCore;

namespace MCVTask.Infrastructure.Context
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(u => u.EmployeeId).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
