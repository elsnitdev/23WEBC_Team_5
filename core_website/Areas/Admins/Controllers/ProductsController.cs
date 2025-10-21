// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using core_website.Areas.Admins.Models;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using core_website.Models;
using core_website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace core_website.Areas.Admins.Controllers
{
  [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IImageProcessingService _imageService;
        private readonly ISanPhamService _sanPhamService;
        private readonly IDanhMucService _danhMucService;
        private readonly IConfiguration _configuration;
        // inject Logger + Service + Hosting Environment
        public ProductsController(
            ILogger<ProductsController> logger,
            IImageProcessingService imageService,
            ISanPhamService sanPhamService,
            IDanhMucService danhMucService,
            IConfiguration configuration
        )
        {
            _logger = logger;
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
                    .ToList();
                return View("Add", new { Message = "Dữ liệu không hợp lệ", Errors = errors });
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
                          destinationPath: _configuration.GetSection("App.Common.ImageUploadFolderPath").ToString(),
                          name: $"{newSanPham.MaSP}_{index}"
                        );
                        newImageFilePaths += Path.GetFileName(newImageFilePath) + ';';
                        index++;
                    }
                }
                newImageFilePaths = newImageFilePaths.TrimEnd(';'); // Xoá dấu chấm phẩy cuối cùng

                // Phân loại sản phẩm nếu có DanhMucId
                if (sanPham.MaDM > 0)
                {
                    _danhMucService.Categorize(newSanPham.MaSP, sanPham.MaDM);
                }

                //return CreatedAtAction(nameof(Get), new { id = newSanPham.MaSP }, sanPham);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating product: {ex.Message}");
            }
        }
    }
}
// KhoaTr - END