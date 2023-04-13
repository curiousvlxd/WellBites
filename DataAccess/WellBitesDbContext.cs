using Microsoft.EntityFrameworkCore;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WellBites.Models;

namespace WellBites.DataAccess
{
    public class WellBitesDbContext : DbContext
    {
        public WellBitesDbContext(DbContextOptions<WellBitesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {   
                entity.Property(u => u.Id)
                    .HasDefaultValueSql("NEWID()");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username)
                    .IsRequired();
                entity.Property(e => e.Email)
                    .IsRequired();
                entity.Property(e => e.Weight)
                    .IsRequired();
                entity.Property(e => e.Height)
                    .IsRequired();
                entity.Property(e => e.Sex)
                    .IsRequired();
                entity.Property(e => e.DateOfBirth)
                    .IsRequired();
                entity.Property(e => e.Activity)
                    .IsRequired();
                entity.Property(e => e.PasswordHash)
                    .IsRequired();
                entity.Property(e => e.PasswordSalt)
                    .IsRequired();
                entity.Property(e => e.Sex)
                    .HasConversion<string>();
                entity.Property(e => e.Activity)
                    .HasConversion<string>();
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}