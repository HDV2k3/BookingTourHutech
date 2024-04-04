using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TourId { get; set; }
        public string TourName { get; set; }    
        public string TourDescription { get; set;}
        public string  ImageTour {  get; set; }       
        public double TourPrice { get; set; }
        public virtual ICollection<DetailBooking> DetailBookings { get; set; } = new List<DetailBooking>();
        [ForeignKey("CategoryTourId")]
        public virtual CategoryTour CategoryTourIdNavigation { get; set; } = null!;


        [ForeignKey("SuplierId")]
        public virtual Suplier SuplierIdNavigation { get; set; } = null!;
    }
}
