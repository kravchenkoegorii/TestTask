using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Persistence.Database
{
    public class TestTaskDbContext : DbContext
    {
        public TestTaskDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>()
                 .HasMany(x => x.Students)
                 .WithOne(x => x.Professor)
                 .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}
