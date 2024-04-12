using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.Areas.Admin.Controllers
{
    public class BookingTourController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
