using BookingTourHutech.Models;

namespace BookingTourHutech.ViewModels
{
    public class CartItem
    {
        
        public int TourId { get; set; }
        public required string ImageTour { get; set; }
        public required string TourName { get; set; }
        public double TourPrice { get; set; }
        public int QuantityPeopele { get; set; }
        public int TimeTour { get; set; }
        public DateTime DayStart { get; set; } = DateTime.Now;
        public DateTime DayEnd { get; set; } = DateTime.Now.AddDays(4);
        public double TotalPriceTour => (QuantityPeopele - 4) * 500000 + TourPrice;
    }
}
