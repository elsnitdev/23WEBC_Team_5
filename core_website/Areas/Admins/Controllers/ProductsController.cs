// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using core_website.Areas.Admins.Models;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using core_website.Models;
using core_website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace core_website.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IImageProcessingService _imageService;
        private readonly IWebHostEnvironment _env;
        private readonly ISanPhamService _sanPhamService;
        private readonly IDanhMucService _danhMucService;
        private readonly IConfiguration _configuration;
        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger,
            IWebHostEnvironment env,
            IImageProcessingService imageService,
            ISanPhamService sanPhamService,
            IDanhMucService danhMucService,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _env = env;
            _imageService = imageService;
            _sanPhamService = sanPhamService;
            _danhMucService = danhMucService;
            _configuration = configuration;
    }

        // GET: Admins/Products/Add
        [HttpGet]
        public IActionResult Add()
        {
            var newProduct = new SanPhamFormViewModel();
            newProduct.DanhMucList = _danhMucService.GetAll().Select(d => new SelectListItem
            {
              Value = d.MaDM.ToString(),
              Text = d.TenDM
            })
            .ToList();
            if (TempData["Message"] != null && TempData["MessageType"] != null)
            {
              newProduct.Message = TempData["Message"]?.ToString();
              newProduct.MessageType = TempData["MessageType"]?.ToString();
            }
            return View(newProduct);
        }
        [HttpPost]
        public async Task<ActionResult<SanPham>> Create([FromForm] SanPhamFormViewModel sanPham)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault();
                TempData["Message"] = $"Dữ liệu gửi lên không hợp lệ ({errors})";
                TempData["MessageType"] = "Failed";

                return RedirectToAction("Add");
            }
            try
            {
                var newSanPham = _sanPhamService.Add(new SanPham()
                {
                    TenSP = sanPham.TenSP,
                    DonGia = sanPham.DonGia,
                    KhuyenMai = sanPham.KhuyenMai ?? 0,
                    MoTa = sanPham.MoTa == null ? "" : sanPham.MoTa,
                    ThongSo = sanPham.ThongSo == null ? "" : sanPham.ThongSo,
                    Tag = sanPham.Tag == null ? "" : sanPham.Tag,
                    SoLuong = sanPham.SoLuong,
                    ThoiGianTao = DateTime.Now,
                    ThoiGianCapNhat = DateTime.Now,
                    TrangThai = true, // Mặc định là true khi tạo mới
                });
                var newImageFilePaths = "";
                int index = 1;
                var HinhAnh = new List<IFormFile>();
                HinhAnh.Add(sanPham.Thumbnail);
                HinhAnh.AddRange(sanPham.HinhAnhChiTiet);

                // Xử lý và lưu các hình ảnh
                foreach (var file in HinhAnh)
                {
                    if (file.Length > 0)
                    {
                        var newImageFilePath = await _imageService.ProcessAndSaveImageAsync(
                          file: file,
                          destinationPath: Path.Combine(_env.WebRootPath, _configuration["AppSettings:Common:ImageUploadFolderPath"]),
                          // Tên là "nămthángngàygiờphútgiâymiligiây_index"
                          name: $"{(DateTime.Now).ToString("yyyyMMddHHmmssfff")}_{index}"
                        );
                        newImageFilePaths += Path.GetFileName(newImageFilePath) + ';';
                        index++;
                    }
                }
                newImageFilePaths = newImageFilePaths.TrimEnd(';'); // Xoá dấu chấm phẩy cuối cùng

                _sanPhamService.UpdateImage(newSanPham.MaSP, newImageFilePaths);

                // Phân loại sản phẩm nếu có DanhMucId
                if (sanPham.MaDM > 0)
                {
                    _danhMucService.Categorize(newSanPham.MaSP, sanPham.MaDM);
                }

                TempData["Message"] = "Sản phẩm thêm thành công";
                TempData["MessageType"] = "Success";

              return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
              TempData["Message"] = $"Đã có lỗi xảy ra: {ex.Message}";
              TempData["MessageType"] = "Failed";

              return RedirectToAction("Add");
            }
        }
    }
}
// KhoaTr - END