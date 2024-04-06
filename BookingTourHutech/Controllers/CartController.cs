
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

        public IActionResult AddToCart(int id, int quantity = 1)
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

                if (model.PaymentMethod == "Thanh Toan Sau")
                {
                    // Xử lý thanh toán bằng COD
                    if (ModelState.IsValid)
                    {

                        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var khachHang = new ApplicationUser();


                        var hoadon = new BookingTour
                        {
                            UserId = customerId ?? khachHang.Id,
                            CustomerName = model.FullName ?? khachHang.FullName,
                            Addresss = model.Address ?? khachHang.Address,
                            CustomerPhone = model.Phone ?? khachHang.PhoneNumber,
                            CustomerEmail = model.Email ?? khachHang.Email,
                            CCCD = model.CCCD,
                            DayStart = DateTime.Now,
                            DayEnd = DateTime.Now.AddDays(3),
                            PaymentMethod = "Thanh Toán Khi Đi",
                            transport = "Xe Khách",
                            StatusId = 1,
                            Note = model.Note,
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
            }
            else { return View("Error"); }
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
    }
}
