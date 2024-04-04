using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        public string StatusName { get; set; } = null!;

        public string? Description { get; set; }
        public virtual ICollection<BookingTour> BookingTours { get; set; } = new List<BookingTour>();
    }
}
