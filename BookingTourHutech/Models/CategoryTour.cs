        using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.Models
{
    public class CategoryTour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryTourId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        public string CategoryNameAlias { get; set; }

        public string Description { get; set; }

        [MaxLength(50)]
        public string Images { get; set; }
        public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    }
}
