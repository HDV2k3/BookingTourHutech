using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.Controllers
{
    public class GalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
