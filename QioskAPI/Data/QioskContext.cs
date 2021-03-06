using QioskAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace QioskAPI.Data
{
    public class QioskContext : DbContext
    {
        public QioskContext(DbContextOptions<QioskContext> options) : base(options)
        {



        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }
        public DbSet<Kiosk> Kiosks { get; set; }
        public DbSet<UserKiosk> UserKiosks { get; set; }
        public DbSet<UserTag> UserTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<User>().HasOne(u=>u.Company);
            modelBuilder.Entity<UserBooking>().HasOne(e => e.User).WithMany().OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Kiosk>().ToTable("Kiosk");
            modelBuilder.Entity<UserKiosk>().ToTable("UserKiosk");
            modelBuilder.Entity<UserTag>().ToTable("UserTag");
   
        }
   
    }
}
