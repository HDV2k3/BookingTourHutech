using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class DetailBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int DetailBookingId { get; set; }

        public int BookingTourId { get; set; }

        public int TourId { get; set; }

        public double TourPrice { get; set; }

        public double Disconut {  get; set; }

        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }

        [ForeignKey("BookingTourId")]
        public virtual BookingTour BookingTour { get; set; }
    }
}
