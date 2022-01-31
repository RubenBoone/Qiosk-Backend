using QioskAPI.Data;
using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI
{
    public class DBinitializer
    {
        public static void Initialize(QioskContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            //Companies
            Company company1 = new Company()
            {
                Name = "VanHout"
            }; Company company2 = new Company()
            {
                Name = "Vanden Borre"
            }; Company company3 = new Company()
            {
                Name = "Gheys NV"
            };

            context.Add(company1);
            context.Add(company2);
            context.Add(company3);

            //Add users
            User u1 = new User()
            {
                FirstName = "User",
                LastName = "One",
                Email = "user1@qiosk.be",
                Password = "G46758/*+DROWSSAPfd656.he6/.",
                IsActive = true,
                IsAdmin = false,
                CompanyID = 1
            };

            User u2 = new User()
            {
                FirstName = "User",
                LastName = "Two",
                Email = "user2@qiosk.be",
                Password = "G46758/*+DROWSSAPfd656.he6/.",
                IsActive = true,
                IsAdmin = true,
                CompanyID= 2

            };

            User u3 = new User()
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "QioskD2@gmail.com",
                Password = "G46758/*+DROWSSAPfd656.he6/.",
                IsActive = true,
                IsAdmin = true,
                CompanyID = 3,
            };

            context.Add(u1);
            context.Add(u2);
            context.Add(u3);
            context.SaveChanges();
            //Add Bookings

            context.Add(new Booking {  BookingTime = DateTime.Now.AddHours(2) , companyID=1});
            context.SaveChanges();
            context.Add(new Booking { BookingTime = DateTime.Now.AddHours(1), companyID=2});
            context.SaveChanges();
           
            //Add UserBookings

            context.Add(new UserBooking { UserID =1 , BookingID =1 });
            context.SaveChanges();
            context.Add(new UserBooking { UserID =2, BookingID = 2 });
            context.SaveChanges();

            //Add Tag

            context.Add(new Tag{ Code = "123ABC" });
            context.SaveChanges();
            context.Add(new Tag { Code = "456DEF" });
            context.SaveChanges();

            //Add UserTags


            context.Add(new UserTag {UserID = 1 ,TagID=1});
            context.SaveChanges();
            context.Add(new UserTag { UserID = 2, TagID= 2 });
            context.SaveChanges();
            
           
            
            //Add Kiosks


            context.Add(new Kiosk {Name = "Workstation" , Description= "WorkStation setup", Coordinate = 0.0});
            context.SaveChanges();
            context.Add(new Kiosk { Name ="Tracking system",Description= "Tracking van mensens bij een beurs", Coordinate=0.0});
            context.SaveChanges();
            
            //Add USerKiosk


            context.Add(new UserKiosk {UserID = 1 ,KioskID=1, Begin = DateTime.Now, End = DateTime.Now.AddMinutes(20)});;
            context.SaveChanges();
            context.Add(new UserKiosk { UserID = 2, KioskID = 1, Begin = DateTime.Now, End = DateTime.Now.AddMinutes(20) });
            context.SaveChanges();
            context.Add(new UserKiosk {UserID = 1, KioskID = 2, Begin = DateTime.Now.AddMinutes(30), End = DateTime.Now.AddMinutes(40) });
            context.SaveChanges();
            context.Add(new UserKiosk { UserID = 2, KioskID = 2, Begin = DateTime.Now.AddMinutes(40), End = DateTime.Now.AddMinutes(45) });
            context.SaveChanges();

           
         
        }
    }
}
