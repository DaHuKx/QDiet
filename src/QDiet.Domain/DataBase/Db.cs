using Microsoft.EntityFrameworkCore;
using QDiet.Domain.Models.DataBase;
using QDiet.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace QDiet.Domain.DataBase
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Resources.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Roles)
                        .WithMany(r => r.Users);
        }
    }
}
