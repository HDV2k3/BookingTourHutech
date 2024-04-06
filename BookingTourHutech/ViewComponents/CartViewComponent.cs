using Microsoft.AspNetCore.Mvc;
using BookingTourHutech.Helpers;
using BookingTourHutech.ViewModels;
namespace BookingTourHutech.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
          var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                QuantityPeople = cart.Sum(p=>p.QuantityPeopele),
                Total = cart.Sum(p=>p.TotalPriceTour)
            });
        }
    }
}
