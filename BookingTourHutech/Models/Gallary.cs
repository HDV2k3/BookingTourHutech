using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class Gallary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdGallary { get; set; }
        public string GallaryName { get; set; }
        public string Images { get; set; }
        public string GallaryDescription { get; set; }

        public string PersonGallary { get; set; }
        public ICollection<GalaryImages> GalaryImages { get; set; }
    }
}
