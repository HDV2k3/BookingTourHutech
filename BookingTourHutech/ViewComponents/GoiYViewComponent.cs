using BookingTourHutech.Models;
using BookingTourHutech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BookingTourHutech.ViewComponents
{
    public class GoiYViewComponent :ViewComponent
    {
        private readonly BookingTourDbContext db;
        public GoiYViewComponent(BookingTourDbContext context) => db = context;

        public  IViewComponentResult Invoke(int? categoryTourId, int? page)
        {
            var tours = db.Tours.AsQueryable();
            tours = tours.OrderBy(t => t.TourId);
            if (categoryTourId.HasValue)
            {
                tours = tours.Where(p => p.CategoryTourId == categoryTourId.Value);
            }
            int pageSize = 6; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            var pagedgoiys = tours.Select(tour => new TourVM
            {
                TourId = tour.TourId,
                ImageTour = tour.ImageTour,
                TourName = tour.TourName,
                TourDescription = tour.TourDescription,
                TourPrice = tour.TourPrice,
                CategoryName = tour.CategoryTourIdNavigation.CategoryName,
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            int totalItems = tours.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            return View( pagedgoiys);
        }
    }
}
