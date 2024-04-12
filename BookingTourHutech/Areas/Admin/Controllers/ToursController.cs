using BookingTourHutech.Models;
using BookingTourHutech.Repository;
using BookingTourHutech.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
namespace BookingTourHutech.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    [Route("admin")]
    [Route("Tours")]
    public class ToursController : Controller
    {
        private readonly ITourRepository _tourRepository;
        private readonly ISuplierRepository _suplierRepository;
        private readonly ICategoryTourRepository _categoryTourRepository;
        public ToursController( ITourRepository tourRepository, ISuplierRepository suplierRepository, ICategoryTourRepository categoryTourRepository)
        {
            _tourRepository = tourRepository;
            _suplierRepository = suplierRepository;
            _categoryTourRepository = categoryTourRepository;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index(int? page)
        {
            var tours = await _tourRepository.GetAllAsync();

            int pageSize = 3; // Số lượng sản phẩm trên mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại
            var pagedTours= tours.Select(tour => new TourVM
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

            return View(pagedTours);
        }
        [Route("Create")]
        // Hiển thị form thêm sản phẩm mới
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryTourRepository.GetAllAsync();
            if (categories == null)
            {
                categories = new List<CategoryTour>(); // khởi tạo một danh sách rỗng nếu loais là null
            }
            ViewBag.CategoryTours = new SelectList(categories, "CategoryTourId", "CategoryTourId");

            var supliers = await _suplierRepository.GetAllAsync();
            if (supliers == null)
            {
                supliers = new List<Suplier>(); // khởi tạo một danh sách rỗng nếu nhaCungCaps là null
            }
            ViewBag.Supliers = new SelectList(supliers, "SuplierId", "SuplierId");

            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Tour tour, IFormFile imageUrl)
        {
            if (!ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
                    tour.ImageTour = await SaveImage(imageUrl);
                }
                await _tourRepository.AddAsync(tour);
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); //Thay đổi đường dẫn theo cấu hình của bạn

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName; // Trả về đường dẫn tương đối
        }
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var tours = await _tourRepository.GetByIdAsync(id);
            if (tours == null)
            {
                return NotFound();
            }
            return View(tours);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var tours = await _tourRepository.GetByIdAsync(id);
            if (tours == null)
            {
                return NotFound();
            }
            var categories = await _categoryTourRepository.GetAllAsync();
            ViewBag.CategoryTours = new SelectList(categories, "CategoryTourId", "CategoryName", tours.CategoryTourId);
            var supliers = await _suplierRepository.GetAllAsync();
            ViewBag.Supliers = new SelectList(supliers, "SuplierId", "SuplierId", tours.SuplierId);

            return View(tours);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Tour tours, IFormFile imageUrl)
        {
            ModelState.Remove("Hinh"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != tours.TourId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var existingProduct = await _tourRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                                                                                 // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên
                if (imageUrl == null)
                {
                    tours.ImageTour = existingProduct.ImageTour;
                }
                else
                {
                    // Lưu hình ảnh mới
                    tours.ImageTour = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingProduct.TourName = tours.TourName;
                existingProduct.TourDescription = tours.TourDescription;
                existingProduct.TourPrice = tours.TourPrice;
                existingProduct.DetailTourDescription = tours.DetailTourDescription;

                await _tourRepository.UpdateAsync(existingProduct);

                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryTourRepository.GetAllAsync();
            ViewBag.CategoryTours = new SelectList(categories, "CategoryTourId", "CategoryName");
            var supliers = await _suplierRepository.GetAllAsync();
            ViewBag.Supliers = new SelectList(supliers, "SuplierId", "SuplierId");
            return View(tours);
        }
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tours = await _tourRepository.GetByIdAsync(id);
            if (tours == null)
            {
                return NotFound();
            }
            return View(tours);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tours = await _tourRepository.GetByIdAsync(id);
            if (tours != null)
            {
                await _tourRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var tours = await _tourRepository.SearchAsync(keyword);
            return View(tours);
        }
    }
}
