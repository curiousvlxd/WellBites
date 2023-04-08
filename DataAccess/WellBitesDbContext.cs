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
        public WellBitesDbContext(DbContextOptions<WellBitesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCharacteristics> UserCharacteristics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Characteristics)
                .WithOne(uc => uc.User)
                .HasForeignKey<UserCharacteristics>(uc => uc.Id);

            modelBuilder.Entity<UserCharacteristics>()
                .HasKey(uc => uc.Id);

            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Weight)
                .IsRequired();

            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Height)
                .IsRequired();

            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Sex)
                .IsRequired();

            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Age)
                .IsRequired();

            modelBuilder.Entity<UserCharacteristics>()
                .Property(uc => uc.Activity)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
