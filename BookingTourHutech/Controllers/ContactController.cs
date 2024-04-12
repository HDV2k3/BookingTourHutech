using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
