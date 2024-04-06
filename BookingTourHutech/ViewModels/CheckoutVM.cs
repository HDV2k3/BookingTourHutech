using System.ComponentModel.DataAnnotations;

namespace BookingTourHutech.ViewModels
{
    public class CheckoutVM
    {
   
		public string? FullName { get; set; }
		//[Required(ErrorMessage = "vui lòng điền đầy đủ thông tin")]
		public string? Address { get; set; }
		//[Required(ErrorMessage = "vui lòng điền đầy đủ thông tin")]
		public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CCCD { get; set; }
        public string? Transport {  get; set; }   
        public string? Note { get; set; }
        public string? PaymentMethod { get; set; }

        public string? Total { get; set; }
    }
}
