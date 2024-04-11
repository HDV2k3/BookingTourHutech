namespace BookingTourHutech.ViewModels
{
	public class BookingTourVM
	{
		public int BookingTourId { get; set; }

		public string CustomerName { get; set; }

		public string CustomerEmail { get; set; }
		public string CustomerPhone { get; set; }

		public string PaymentMethod { get; set; }

		public string ActivationMethod { get; set; }
		public string Note { get; set; }
		public int PeopleCount { get; set; }

		public DateTime DayStart { get; set; }

		public DateTime DayEnd { get; set; }

	}
}
