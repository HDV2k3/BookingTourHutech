using Microsoft.EntityFrameworkCore;

namespace BookingTourHutech.Models
{
    public partial class BookingTourDbContext  :DbContext
    {
        public BookingTourDbContext(DbContextOptions<BookingTourDbContext> options)
        : base(options)
        {

        }
        public DbSet<DetailBooking> DetailBookings { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<BookingTour> BookingTours { get; set; }

        public DbSet<CategoryTour> CategoryTours { get; set; }
        public DbSet<Suplier> Supliers { get; set; }

    }
}
