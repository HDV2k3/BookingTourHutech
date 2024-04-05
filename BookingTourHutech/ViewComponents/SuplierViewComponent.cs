using BookingTourHutech.Models;
using BookingTourHutech.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.ViewComponents
{
    public class SuplierViewComponent :ViewComponent
    {
        private readonly BookingTourDbContext db;
        public SuplierViewComponent(BookingTourDbContext context) => db = context;

        public  IViewComponentResult Invoke()
        {
            var data  = db.Supliers.Select(suplier=> new SuplierVM
            {
                SuplierId = suplier.SuplierId,
                SuplierName =suplier.SuplierName,
                
            }).OrderBy(p=>p.SuplierName).ToList();  
            return View(data);
        }
    }
}
