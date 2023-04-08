using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellBites.Models;

namespace WellBites.DataAccess
{
    public  class WellBitesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserCharacteristics> UserCharacteristics { get; set; }


        public WellBitesDbContext(DbContextOptions<WellBitesDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<User>()
                .HasOne(u => u.Characteristics)
                .WithOne()
                .HasForeignKey<UserCharacteristics>(uc => uc.Id);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserCharacteristics>()
                .HasIndex(uc => uc.Id)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Id)
                .IsRequired();
            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Age)
                .IsRequired();
            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Height)
                .IsRequired();
            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Weight)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
