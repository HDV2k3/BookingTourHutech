
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
using BookingTourHutech.Momo;

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
                            StatusId = 0,
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
                else if (model.PaymentMethod == "Thanh Toán Momo")
                {
                    //request params need to request to MoMo system
                    string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
                    string partnerCode = "MOMOOJOI20210710";
                    string accessKey = "iPXneGmrJH0G8FOP";
                    string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
                    string orderInfo = "MotoBike Shop";
                    string returnUrl = "https://localhost:44386/Cart/Success";
                    string notifyurl = "https://localhost:44386/Cart/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
                    string amount = model.Total.Replace(".", "").Replace(",", "").Replace("VND", "");
                    string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
                    string requestId = DateTime.Now.Ticks.ToString();
                    string extraData = "";

                    //Before sign HMAC SHA256 signature
                    string rawHash = "partnerCode=" +
                        partnerCode + "&accessKey=" +
                        accessKey + "&requestId=" +
                        requestId + "&amount=" +
                        amount + "&orderId=" +
                        orderid + "&orderInfo=" +
                        orderInfo + "&returnUrl=" +
                        returnUrl + "&notifyUrl=" +
                        notifyurl + "&extraData=" +
                        extraData;

                    MoMoSecurity crypto = new MoMoSecurity();
                    //sign signature SHA256
                    string signature = crypto.signSHA256(rawHash, serectkey);

                    //build body json request
                    JObject message = new JObject
                    {
                    { "partnerCode", partnerCode },
                    { "accessKey", accessKey },
                    { "requestId", requestId },
                    { "amount", amount },
                    { "orderId", orderid },
                    { "orderInfo", orderInfo },
                    { "returnUrl", returnUrl },
                    { "notifyUrl", notifyurl },
                    { "extraData", extraData },
                    { "requestType", "captureMoMoWallet" },
                    { "signature", signature }
                    };

                    string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

                    JObject jmessage = JObject.Parse(responseFromMomo);

                    if (jmessage.TryGetValue("payUrl", out JToken payUrlToken))
                    {
                        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var khachHang = new ApplicationUser();
                     
                        var hoadon = new BookingTour
                        {
                            CustomerName = model.FullName ?? khachHang.FullName,
                            CustomerEmail = model.Email ?? khachHang.Email,
                            CustomerPhone = model.Phone ?? khachHang.PhoneNumber,
                            PaymentMethod = "Thanh Toán MoMo",
                            transport = "Xe Khách",
                            Note = model.Note,
                            StatusId = 0,
                            DayStart = DateTime.Now.AddDays(1),
                            DayEnd = DateTime.Now.AddDays(5),
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

                            return Redirect(payUrlToken.ToString());
                        }
                        catch
                        {
                            db.Database.RollbackTransaction();
                            return View("Error");
                        }
                    }
                    else
                    {
                        // Handle error when "payUrl" does not exist in the response
                        // For example, you may want to redirect to an error page or back to the checkout form
                        return View("Error");
                    }

                }
            }
            else 
            { 
                
                return View("Error");
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
        public IActionResult Down1(int id)
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

            return RedirectToAction("Checkout");
        }
        // tăng số lượng
        public IActionResult Up1(int id)
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

            return RedirectToAction("Checkout");
        }
    }
}
