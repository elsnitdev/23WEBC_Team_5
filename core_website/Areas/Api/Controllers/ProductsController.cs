// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
using core_website.Areas.Admins.Services;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using core_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ISanPhamService _sanPhamService;
    private readonly IWebHostEnvironment _env;
    private readonly IImageProcessingService _imageService;
    public ProductsController(
      ISanPhamService sanPhamService,
      IWebHostEnvironment env,
      IImageProcessingService imageService
    )
    {
      _sanPhamService = sanPhamService;
      _env = env;
      _imageService = imageService;
    }

    // GET: api/Products
    [HttpGet]
    public ActionResult<IEnumerable<SanPham>> Get()
    {
      try
      {
        var sanPhams = _sanPhamService.GetAll();
        return Ok(sanPhams);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error retrieving products: {ex.Message}");
      }
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public ActionResult<SanPham> Get(int id)
    {
      try
      {
        var sanPham = _sanPhamService.GetById(id);
        if (sanPham == null)
        {
          return NotFound();
        }
        return Ok(sanPham);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error retrieving product: {ex.Message}");
      }
    }

    // POST: api/Products
    [HttpPost] 
    public async Task<ActionResult<SanPham>> Post([FromForm] SanPhamViewModel sanPham)
    {
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(new { Message = "Validation failed", Errors = errors });
      }
      try
      {
        int newMaSP = _sanPhamService.GetLastestProductId() + 1;
        var newImageFilePaths = "";
        int index = 1;
        foreach (var file in sanPham.HinhAnh)
        {
          if (file.Length > 0)
          {
            var newImageFilePath = await _imageService.ProcessAndSaveImageAsync(
              file: file,
              destinationPath: Path.Combine(_env.WebRootPath, "images"),
              name: $"{newMaSP}_{index}"
            );
            newImageFilePaths += Path.GetFileName(newImageFilePath) + ';';
            index++;
          }
        }
        newImageFilePaths = newImageFilePaths.TrimEnd(';'); // Xoá dấu chấm phẩy cuối cùng

        var newSanPham = new SanPham()
        {
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
        _sanPhamService.Add(newSanPham);
        return CreatedAtAction(nameof(Get), new { id = newSanPham.MaSP }, sanPham);
      }
      catch (Exception ex)
      {
        return BadRequest($"Error creating product: {ex.Message}");
      }
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] SanPham sanPham)
    {
      try
      {
        var existingSanPham = _sanPhamService.GetById(id);
        if (existingSanPham == null)
        {
          return NotFound();
        }
        sanPham.MaSP = id;
        sanPham.ThoiGianCapNhat = DateTime.Now;
        _sanPhamService.Update(sanPham);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error updating product: {ex.Message}");
      }
    }

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