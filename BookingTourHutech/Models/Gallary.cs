using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class Gallary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdGallary { get; set; }
        public string Images { get; set; }
    }
}
