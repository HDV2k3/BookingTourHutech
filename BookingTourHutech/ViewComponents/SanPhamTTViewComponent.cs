using BookingTourHutech.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MotoBikeShop.ViewComponents
{
	public class SanPhamTTViewComponent : ViewComponent
	{
		private readonly BookingTourDbContext db;

		public SanPhamTTViewComponent(BookingTourDbContext context) => db = context;

        public IViewComponentResult Invoke(int maloai)
        {
            // Lấy danh sách sản phẩm có cùng mã loại
            var products = GetProductsByMaloai(maloai);

            // Trả về view và truyền danh sách sản phẩm
            return View(products);
        }

        public List<Tour> GetProductsByMaloai(int maloai)
        {
            return db.Tours.Where(p => p.TourId == maloai).ToList();
        }
    }
}