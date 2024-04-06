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
        public double TotalPriceTour => QuantityPeopele * TourPrice;
    }
}
