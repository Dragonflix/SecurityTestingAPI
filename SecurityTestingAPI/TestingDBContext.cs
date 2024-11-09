using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SecurityTestingAPI.Models;

namespace SecurityTestingAPI
{
    public class TestingDBContext: DbContext
    {
        public TestingDBContext()
        {
        }

        public TestingDBContext(DbContextOptions<TestingDBContext> options)
            : base(options)
        {
        }

        public DbSet<Complexity> Complexities { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TestTask> TestTasks { get; set; }

        public DbSet<CompletedTask> CompletedTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complexity>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Role>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<TaskType>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<TestTask>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<CompletedTask>()
                .HasKey(e => new {e.UserId, e.TaskId});
        }
    }
}
