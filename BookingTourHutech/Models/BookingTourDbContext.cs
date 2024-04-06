
using BookingTourHutech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingTourHutech.Models
{
    public partial class BookingTourDbContext  :IdentityDbContext
    {
        public BookingTourDbContext(DbContextOptions<BookingTourDbContext> options)
        : base(options)
        {

        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<DetailBooking> DetailBookings { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<BookingTour> BookingTours { get; set; }

        public DbSet<CategoryTour> CategoryTours { get; set; }
        public DbSet<Suplier> Supliers { get; set; }

        public DbSet<Gallary> gallaries { get; set; }

        public DbSet<GalaryImages> GalaryImages { get; set;}

        public DbSet<TourImages> TourImages { get; set; }

    }
}
