using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class BookingTour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingTourId { get; set; }

        public string CustomerName { get; set; }

        public string CCCD { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

        public string PaymentMethod {  get; set; }

        public string ActivationMethod { get; set; }
        public string Note { get; set; }
        public int PeopleCount { get; set; }

        public DateTime DayStart   { get; set; }
     
        public DateTime DayEnd { get; set; }

        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual ApplicationUser User { get; set; }
        public virtual ICollection<DetailBooking> DetailBookings { get; set; } = new List<DetailBooking>();
    }
}
