// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using Microsoft.AspNetCore.Mvc;
using core_website.Services;
using core_website.Models;
using core_website.Areas.Admins.Services;

namespace core_website.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ISanPhamService _sanPhamService;
        private readonly IWebHostEnvironment _env;
        private readonly IImageProcessingService _imageService;

        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger,
            ISanPhamService sanPhamService,
            IWebHostEnvironment env,
            IImageProcessingService imageService
         )
        {
            _logger = logger;
            _sanPhamService = sanPhamService;
            _env = env;
            _imageService = imageService;
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

            // Validate
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState không hợp lệ");
                return View("Add", sanPham);
            }

            var newList = _sanPhamService.GetAll();
            int newMaSP = (newList.Count > 0) ? newList.Max(sp => sp.MaSP) + 1 : 1;

            // KhoaTr - 7/10/2025: Upload hình
            var newImageFilePaths = "";
            int index = 1;
            foreach (var file in sanPham.HinhAnh)
            {
              if (file.Length > 0)
              {
                var newImageFilePath = await _imageService.ProcessAndSaveImageAsync(
                  file: file,
                  destinationPath: Path.Combine(_env.WebRootPath, "uploads"),
                  name: $"{newMaSP}_{index}"
                );
                newImageFilePaths += newImageFilePath + ';';
                index++;
              }
            }
            newImageFilePaths = newImageFilePaths.TrimEnd(';'); // Xoá dấu chấm phẩy cuối cùng

            // Lưu DB
            var newSanPham = new SanPham() {
              MaSP = newMaSP,
              TenSP = sanPham.TenSP,
              DonGia = sanPham.DonGia,
              KhuyenMai = sanPham.KhuyenMai ?? 0,
              MoTa = sanPham.MoTa == null ? "" : sanPham.MoTa,
              ThongSo = sanPham.ThongSo == null ? "" : sanPham.ThongSo,
              Tag = sanPham.Tag == null ? "" : sanPham.Tag,
              SoLuong = sanPham.SoLuong,
              HinhAnh = newImageFilePaths,
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
// KhoaTr - END