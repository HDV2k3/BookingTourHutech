using BookingTourHutech.Models;
using BookingTourHutech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace BookingTourHutech.Controllers
{
    public class GoiYController : Controller
    {
        private readonly BookingTourDbContext db;
        public GoiYController(BookingTourDbContext context)
        {
            db = context;
        }

       
       
        public IActionResult Detail(int id)
        {
			var data = db.Tours
				.Include(p => p.CategoryTourIdNavigation)
				.SingleOrDefault(p => p.TourId == id);
			if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new DetailTourVM
            {
                CategoryId = data.CategoryTourId,
                TourId = data.TourId,
                TourName = data.TourName,
                TourDescription = data.TourDescription,
                ImageTour = data.ImageTour,
                TourPrice = data.TourPrice,
                //CategoryName = data.CategoryTourIdNavigation.CategoryName,
            };
            return View(result);
        }

        public IActionResult Index(int? categoryTourId, int? page)
        {
            var tours = db.Tours.AsQueryable();
            tours = tours.OrderBy(t => t.TourId);

            if (categoryTourId.HasValue)
            {
                tours = tours.Where(p => p.CategoryTourId == categoryTourId.Value);
            }

            int pageSize = 3; // Số lượng sản phẩm trên mỗi trang
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

            return View(pagedgoiys);
        }
    }
}