using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkshopOne.Common.Entities;

namespace WorkshopOne.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Countryside> countrysides { get; set; }
        public DbSet<church> churches { get; set; }
        public DbSet<District> districts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Countryside>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<church>()
                .HasIndex(C => C.Name)
                .IsUnique();

            modelBuilder.Entity<District>()
                .HasIndex(D => D.Name)
                .IsUnique();
        }
    }
}
