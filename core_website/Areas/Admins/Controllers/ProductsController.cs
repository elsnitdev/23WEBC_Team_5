using Microsoft.AspNetCore.Mvc;
using core_website.Services;
using core_website.Models;

namespace core_website.Areas.Admins.Controllers
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
            return View(new SanPhamViewModel());
        }

        // POST: Api/Products/Create
        [HttpPost]
        [Route("Api/Products/Create")]
        public async Task<IActionResult> Create([FromForm] SanPhamViewModel sanPham, IFormFile? hinhAnh)
        {
            _logger.LogInformation("Nhận POST request /Api/Products/Create");

            // Log dữ liệu sản phẩm từ form
            _logger.LogInformation("Tên SP: {TenSP}", sanPham.TenSP);
            _logger.LogInformation("Đơn giá: {DonGia}", sanPham.DonGia);
            _logger.LogInformation("KM: {KhuyenMai}", sanPham.KhuyenMai);
            _logger.LogInformation("Mô tả: {MoTa}", sanPham.MoTa);
            _logger.LogInformation("Tag: {LoaiSP}", sanPham.Tag);
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
            var newList = _sanPhamService.GetAll();
            int newMaSP = (newList.Count > 0) ? newList.Max(sp => sp.MaSP) + 1 : 1;
            var newSanPham = new SanPham() {
              MaSP = newMaSP,
              TenSP = sanPham.TenSP,
              DonGia = sanPham.DonGia,
              KhuyenMai = sanPham.KhuyenMai ?? 0,
              MoTa = sanPham.MoTa,
              ThongSo = sanPham.ThongSo,
              Tag = sanPham.Tag,
              SoLuong = sanPham.SoLuong,
              HinhAnh = sanPham.HinhAnh,
              ThoiGianTao = DateTime.Now,
              ThoiGianCapNhat = DateTime.Now,
              TrangThai = true // Mặc định là true khi tạo mới
            };
            newList.Add(newSanPham);
            _sanPhamService.UpdateList(newList);
            _logger.LogInformation("Đã lưu sản phẩm thành công: {TenSP}", sanPham.TenSP);

            TempData["SuccessMessage"] = "Tạo sản phẩm thành công";
            return RedirectToAction("Add");
        }

    }
}
