using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.Areas.Admin.Controllers
{
    public class GalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
