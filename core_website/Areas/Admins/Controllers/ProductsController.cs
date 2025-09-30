using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using core_w2.Areas.Admins.Services;
using core_w2.Models;

namespace core_w2.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ISanPhamService _sanPhamService;
        private readonly IWebHostEnvironment _env;

        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger,
            ISanPhamService sanPhamService,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _sanPhamService = sanPhamService;
            _env = env;
        }

        // GET: Admins/Products/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View(new SanPham());
        }

        // POST: Api/Products/Create
        [HttpPost]
        [Route("Api/Products/Create")]
        public async Task<IActionResult> Create([FromForm] SanPham sanPham, IFormFile? hinhAnh)
        {
            _logger.LogInformation("Nhận POST request /Api/Products/Create");

            // Log dữ liệu sản phẩm từ form
            _logger.LogInformation("Tên SP: {TenSP}", sanPham.TenSP);
            _logger.LogInformation("Đơn giá: {DonGia}", sanPham.DonGia);
            _logger.LogInformation("Đơn giá KM: {DonGiaKhuyenMai}", sanPham.DonGiaKhuyenMai);
            _logger.LogInformation("Mô tả: {MoTa}", sanPham.MoTa);
            _logger.LogInformation("Loại SP: {LoaiSP}", sanPham.LoaiSP);
            _logger.LogInformation("Ảnh (form binding): {HinhAnh}", sanPham.HinhAnh);

            if (hinhAnh != null)
            {
                _logger.LogInformation("File upload: {FileName}, size: {Size} bytes", hinhAnh.FileName, hinhAnh.Length);
            }
            else
            {
                _logger.LogWarning("Không có file ảnh nào được upload");
            }

            // Validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("Add", sanPham);
            }

            // Upload ảnh
            if (hinhAnh != null && hinhAnh.Length > 0)
            {
                var fileName = Path.GetFileName(hinhAnh.FileName);
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await hinhAnh.CopyToAsync(stream);
                }

                sanPham.HinhAnh = "/uploads/" + fileName;
                _logger.LogInformation("Đã lưu ảnh vào {FilePath}", filePath);
            }

            // Lưu DB
            _sanPhamService.LuuSanPham(sanPham);
            _logger.LogInformation("Đã lưu sản phẩm thành công: {TenSP}", sanPham.TenSP);

            TempData["SuccessMessage"] = "Tạo sản phẩm thành công";
            return RedirectToAction("Add");
        }

    }
}
