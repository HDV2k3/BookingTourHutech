using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
