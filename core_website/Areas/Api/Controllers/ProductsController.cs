// KhoaTr - 5/10/2025: Sửa lại model + namespace + logic xử lý
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
    public ProductsController(ISanPhamService sanPhamService)
    {
      _sanPhamService = sanPhamService;
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
    public ActionResult<SanPham> Post([FromBody] SanPham sanPham)
    {
      try
      {
        sanPham.ThoiGianTao = DateTime.Now;
        sanPham.ThoiGianCapNhat = DateTime.Now;
        _sanPhamService.Add(sanPham);
        return CreatedAtAction(nameof(Get), new { id = sanPham.MaSP }, sanPham);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error creating product: {ex.Message}");
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