using BookingTourHutech.Models;
using Microsoft.AspNetCore.Mvc;
using BookingTourHutech.Models;
using BookingTourHutech.ViewModels;

namespace BookingTourHutech.ViewComponents
{
    public class GalaryViewComponent : ViewComponent
    {
        private readonly BookingTourDbContext db;

        public GalaryViewComponent(BookingTourDbContext context) => db = context;

        public IViewComponentResult Invoke(int? page)
        {
            var galarys = db.gallaries.AsQueryable();
            int pageSize = 6; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            var pagedGalaries = db.gallaries.Select(galary => new GallaryVM
            {
                Images = galary.Images,
            }).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            int totalItems = galarys.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            return View(pagedGalaries);
        }
    }
}
