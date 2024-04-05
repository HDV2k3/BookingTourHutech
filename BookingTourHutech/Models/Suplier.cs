using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingTourHutech.Models
{
    public class Suplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int SuplierId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SuplierName { get; set; }

       
        // tên người đại diện
        [MaxLength(50)]
        public string deputyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string PhoneSuplier { get; set; }

        [MaxLength(50)]
        public string AddressSuplier { get; set; }

        public string DescriptionSuplier { get; set; }

        public string ImageSuplier { get; set; }
    }
}
