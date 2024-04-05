using BookingTourHutech.Models;
using BookingTourHutech.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingTourHutech.ViewComponents
{
    public class CategoryViewComponent :ViewComponent
    {
        private readonly BookingTourDbContext db;

        public CategoryViewComponent(BookingTourDbContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.CategoryTours.Select(category=>new CategoryTourVM
            {
                CategoryTourId = category.CategoryTourId,
                CategoryName = category.CategoryName,
                TotalCategory = category.Tours.Count()
            }).OrderBy(p=>p.CategoryName).ToList();
            return View(data);
        }
    }
}
