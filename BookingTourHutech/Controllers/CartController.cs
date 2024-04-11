﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookingTourHutech.Helpers;
using BookingTourHutech.Models;
using BookingTourHutech.Repository;
using BookingTourHutech.ViewModels;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BookingTourHutech.Controllers
{
    public class CartController : Controller
    {
        private readonly BookingTourDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(BookingTourDbContext context, UserManager<ApplicationUser> userManager)
        {
          
            db = context;
            _userManager = userManager;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 4)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.TourId == id);
            if (item == null)
            {
                var hangHoa = db.Tours.SingleOrDefault(p => p.TourId == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    TourId = hangHoa.TourId,
                    TourName = hangHoa.TourName,
                    TourPrice = hangHoa.TourPrice,
                    ImageTour = hangHoa.ImageTour ?? string.Empty,
                    QuantityPeopele = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.QuantityPeopele += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);


            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.TourId == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }

            return View(Cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {

                if (model.PaymentMethod == "Thanh Toán Khi Đi")
                {
                    // Xử lý thanh toán bằng COD
                    if (ModelState.IsValid)
                    {

                        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var khachHang = new ApplicationUser();


                        var hoadon = new BookingTour
                        {
                            CustomerName = model.FullName ?? khachHang.FullName,
							CustomerEmail = model.Email ?? khachHang.Email,
							CustomerPhone = model.Phone ?? khachHang.PhoneNumber,
							PaymentMethod = "Thanh Toán Khi Đi",
							transport = "Xe Khách",
							Note = model.Note,
							StatusId = 1,
							DayStart = DateTime.Now,
							DayEnd = DateTime.Now.AddDays(3),                        
							UserId = customerId ?? khachHang.Id,
							Addresss = model.Addresss ?? khachHang.Address,            
                        };
                        db.Database.BeginTransaction();
                        try
                        {
                            db.Add(hoadon);
                            db.SaveChanges();

                            var cthds = new List<DetailBooking>();
                            foreach (var item in Cart)
                            {
                                cthds.Add(new DetailBooking
                                {
                                    BookingTourId = hoadon.BookingTourId,
                                    PersonCount = item.QuantityPeopele,
                                    TourPrice = item.TourPrice,
                                    TourId = item.TourId,
                                    Disconut = 0
                                });
                            }
                            db.AddRange(cthds);
                            db.SaveChanges();

                            db.Database.CommitTransaction();

                            HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

                            return View("Success");
                        }
                        catch
                        {
                            return View("Error");
                            db.Database.RollbackTransaction();
                        }
                    }

                    return View(Cart);
                }
				else { return View("Error"); }
			}
           
            return View(model);
        }



        [Authorize]

        public IActionResult Success()
        {
            return View();
        }
        [Authorize]

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Down(int id)
        {
            var giohang = Cart;
            CartItem item = giohang.Where(p => p.TourId == id).FirstOrDefault();
            if (item.QuantityPeopele > 1)
            {
                --item.QuantityPeopele;
            }
            else
            {
                giohang.RemoveAll(p => p.TourId == id);
            }
            if (giohang.Count == 0)
            {
                HttpContext.Session.Remove(MySetting.CART_KEY);
            }
            else
            {
                HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            }

            return RedirectToAction("Index");
        }
        // tăng số lượng
        public IActionResult Up(int id)
        {
            var giohang = Cart;
            CartItem item = giohang.Where(p => p.TourId == id).FirstOrDefault();
            if (item.QuantityPeopele > 1)
            {
                ++item.QuantityPeopele;
            }
            else
            {
                giohang.RemoveAll(p => p.TourId == id);
            }
            if (giohang.Count == 0)
            {
                HttpContext.Session.Remove(MySetting.CART_KEY);
            }
            else
            {
                HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            }

            return RedirectToAction("Index");
        }
    }
}
