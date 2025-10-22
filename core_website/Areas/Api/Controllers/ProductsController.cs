// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using core_website.Areas.Admins.Models;
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ISanPhamService _sanPhamService;
    private readonly IDanhMucService _danhMucService;
    private readonly IWebHostEnvironment _env;
    private readonly IImageProcessingService _imageService;
    // Khởi tạo
    public ProductsController(
      ISanPhamService sanPhamService,
      IDanhMucService danhMucService,
      IWebHostEnvironment env,
      IImageProcessingService imageService
    )
    {
      _sanPhamService = sanPhamService;
      _danhMucService = danhMucService;
      _env = env;
      _imageService = imageService;
    }

    // GET: api/Products?itemsPerPage=8
    [HttpGet]
    public ActionResult<IEnumerable<Object>> Get([FromQuery] int? itemsPerPage)
    {
      try
      {
        // Lấy tất cả sản phẩm
        // 
        var sanPhams = _sanPhamService.GetAll(itemsPerPage);
        return Ok(sanPhams);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Lỗi khi lấy Sản Phẩm: {ex.Message}");
      }
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public ActionResult<Object> Get(int id)
    {
      try
      {
        // Lấy sản phẩm theo MaSP (ID)
        var sanPham = _sanPhamService.GetById(id);
        if (sanPham == null)
        {
          return NotFound();
        }
        return Ok(sanPham);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Lỗi khi lấy sản phẩm: {ex.Message}");
      }
    }

    // POST: api/Products
    [HttpPost] 
    //public async Task<ActionResult<SanPham>> Post([FromForm] SanPhamFormViewModel sanPham)
    //{
    //  // Kiểm tra tính hợp lệ của dữ liệu đầu vào
    //  if (!ModelState.IsValid)
    //  {
    //    var errors = ModelState.Values
    //        .SelectMany(v => v.Errors)
    //        .Select(e => e.ErrorMessage)
    //        .ToList();
    //    return BadRequest(new { Message = "Dữ liệu không hợp lệ", Errors = errors });
    //  }
    //  try
    //  {
    //    int newMaSP = _sanPhamService.GetLastestProductId() + 1;
    //    var newImageFilePaths = "";
    //    int index = 1;

    //    // Xử lý và lưu các hình ảnh
    //    foreach (var file in sanPham.HinhAnh)
    //    {
    //      if (file.Length > 0)
    //      {
    //        var shortenedProductName = string.Join("", sanPham.TenSP.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(w => char.ToUpper(w[0])).ToList());
    //        var newImageFilePath = await _imageService.ProcessAndSaveImageAsync(
    //          file: file,
    //          destinationPath: Path.Combine(_env.WebRootPath, "images"),
    //          name: $"{shortenedProductName}_{index}"
    //        );
    //        newImageFilePaths += Path.GetFileName(newImageFilePath) + ';';
    //        index++;
    //      }
    //    }
    //    newImageFilePaths = newImageFilePaths.TrimEnd(';'); // Xoá dấu chấm phẩy cuối cùng

    //    var newSanPham = new SanPham()
    //    {
    //      MaSP = newMaSP,
    //      TenSP = sanPham.TenSP,
    //      DonGia = sanPham.DonGia,
    //      KhuyenMai = sanPham.KhuyenMai ?? 0,
    //      MoTa = sanPham.MoTa == null ? "" : sanPham.MoTa,
    //      ThongSo = sanPham.ThongSo == null ? "" : sanPham.ThongSo,
    //      Tag = sanPham.Tag == null ? "" : sanPham.Tag,
    //      SoLuong = sanPham.SoLuong,
    //      HinhAnh = newImageFilePaths,
    //      ThoiGianTao = DateTime.Now,
    //      ThoiGianCapNhat = DateTime.Now,
    //      TrangThai = true, // Mặc định là true khi tạo mới
    //    };
    //    _sanPhamService.Add(newSanPham);

    //    // Phân loại sản phẩm nếu có DanhMucId
    //    if (sanPham.MaDM > 0)
    //    {
    //      _danhMucService.Categorize(newMaSP, sanPham.MaDM);
    //    }

    //    return CreatedAtAction(nameof(Get), new { id = newSanPham.MaSP }, sanPham);
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest($"Error creating product: {ex.Message}");
    //  }
    //}

    // PUT: api/Products/5
    //[HttpPut("{id}")]
    //public ActionResult Put(int id, [FromBody] SanPham sanPham)
    //{
    //  try
    //  {
    //    var existingSanPham = _sanPhamService.GetById(id);
    //    if (existingSanPham == null)
    //    {
    //      return NotFound();
    //    }
    //    sanPham.MaSP = id;
    //    sanPham.ThoiGianCapNhat = DateTime.Now;
    //    _sanPhamService.Update(sanPham);
    //    return NoContent();
    //  }
    //  catch (Exception ex)
    //  {
    //    return StatusCode(500, $"Error updating product: {ex.Message}");
    //  }
    //}

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      try
      {
        var sanPham = _sanPhamService.GetById(id);
        if (sanPham == null)
        {
          return NotFound();
        }
        _sanPhamService.Delete(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error deleting product: {ex.Message}");
      }
    }
  }
}
// KhoaTr - END