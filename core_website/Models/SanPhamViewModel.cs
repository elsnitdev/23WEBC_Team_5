using System.ComponentModel.DataAnnotations;

namespace core_website.Models
{
    public class SanPhamViewModel
    {
      [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
      [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên sản phẩm phải từ 3 đến 100 ký tự")]
      [Display(Name = "Tên sản phẩm")]
      public string TenSP { get; set; }

      [Required(ErrorMessage = "Đơn giá là bắt buộc")]
      [Range(1000, 100000000, ErrorMessage = "Đơn giá phải từ 1,000 đến 100,000,000 VNĐ")]
      [Display(Name = "Đơn giá")]
      [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
      public decimal DonGia { get; set; }

      [Range(0, 100000000, ErrorMessage = "Khuyến mãi phải từ 0 đến 100,000,000 VNĐ")]
      [Display(Name = "Khuyến mãi")]
      [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = false)]
      public decimal? KhuyenMai { get; set; }

      [StringLength(5000, ErrorMessage = "Mô tả không được vượt quá 5000 ký tự")]
      [Display(Name = "Mô tả")]
      [DataType(DataType.MultilineText)]
      public string? MoTa { get; set; }

      [StringLength(5000, ErrorMessage = "Thông số kỹ thuật không được vượt quá 5000 ký tự")]
      [Display(Name = "Thông số kỹ thuật")]
      [DataType(DataType.MultilineText)]
      public string? ThongSo { get; set; }

      [StringLength(100, ErrorMessage = "Tag không được vượt quá 100 ký tự")]
      [Display(Name = "Tag")]
      public string? Tag { get; set; }

      [Required(ErrorMessage = "Số lượng là bắt buộc")]
      [Range(1, 10000, ErrorMessage = "Số lượng phải từ 1 đến 10,000")]
      [Display(Name = "Số lượng")]
      public int SoLuong { get; set; }

      [Required(ErrorMessage = "Hình ảnh là bắt buộc")]
      [StringLength(2000, ErrorMessage = "Đường dẫn hình ảnh không được vượt quá 2000 ký tự")]
      [Display(Name = "Hình ảnh")]
      [DataType(DataType.Upload)]
      public IFormFile[] HinhAnh { get; set; }
    }
  }