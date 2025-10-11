using Microsoft.AspNetCore.Mvc;
using core_website.Areas.Api.Models;
using core_website.Areas.Api.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace core_website.Areas.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly IDanhMucService _danhMucService;

    public CategoriesController(IDanhMucService danhMucService)
    {
      _danhMucService = danhMucService;
    }

    // GET: api/Categories
    [HttpGet]
    public ActionResult<IEnumerable<DanhMuc>> Get()
    {
      try
      {
        var danhMucs = _danhMucService.GetAll();
        return Ok(danhMucs);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error retrieving categories: {ex.Message}");
      }
    }

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public ActionResult<DanhMuc> Get(int id)
    {
      try
      {
        var danhMuc = _danhMucService.GetById(id);
        if (danhMuc == null)
        {
          return NotFound();
        }
        return Ok(danhMuc);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error retrieving category: {ex.Message}");
      }
    }

    // POST: api/Categories
    [HttpPost]
    public ActionResult<DanhMuc> Post([FromBody] DanhMuc danhMuc)
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
        _danhMucService.Add(danhMuc);
        return CreatedAtAction(nameof(Get), new { id = danhMuc.MaDM }, danhMuc);
      }
      catch (Exception ex)
      {
        return BadRequest($"Error creating category: {ex.Message}");
      }
    }
    // POST: api/Categories/Categorize
    [HttpPost("Categorize")]
    public ActionResult Categorize([FromBody] PhanLoai phanLoai)
    {
      try
      {
        _danhMucService.Categorize(phanLoai.MaSP, phanLoai.MaDM);
        return Ok(new { Message = "Products categorized successfully" });
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error categorizing products: {ex.Message}");
      }
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] DanhMuc danhMuc)
    {
      try
      {
        if (id != danhMuc.MaDM)
        {
          return BadRequest(new { Message = "Category ID mismatch" });
        }

        var existingDanhMuc = _danhMucService.GetById(id);
        if (existingDanhMuc == null)
        {
          return NotFound();
        }

        _danhMucService.Update(danhMuc);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Error updating category: {ex.Message}");
      }
    }
  }
}