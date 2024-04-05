namespace BookingTourHutech.ViewModels
{
	public class TourVM
	{
		public int TourId { get; set; }
		public string TourName { get; set; }
		public string TourDescription { get; set; }
		public string ImageTour { get; set; }
		public double TourPrice { get; set; }
		public string CategoryName { get; set; }

	}

	public class DetailTourVM
	{
		public int TourId { get; set; }
		public string TourName { get; set; }
		public string TourDescription { get; set; }
		public string ImageTour { get; set; }
		public double TourPrice { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}
