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
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Resources.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Roles)
                        .WithMany(r => r.Users);

            modelBuilder.Entity<Blog>()
                        .HasOne(b => b.Owner)
                        .WithOne(u => u.Blog);

            modelBuilder.Entity<Blog>()
                        .HasMany(b => b.Posts)
                        .WithOne(p => p.Blog)
                        .HasForeignKey(p => p.BlogId);

            modelBuilder.Entity<Blog>()
                        .HasMany(b => b.Subscribers)
                        .WithMany(u => u.Blogs);
        }
    }
}
