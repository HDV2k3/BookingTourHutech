using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.ViewModels
{
	public class CategoryTourVM
	{
		public int CategoryTourId { get; set; }
		[Required]
		[MaxLength(50)]
		public string CategoryName { get; set; }

		[MaxLength(50)]
		public string CategoryNameAlias { get; set; }

		public string Description { get; set; }

		[MaxLength(50)]
		public string Images { get; set; }
	}
}
