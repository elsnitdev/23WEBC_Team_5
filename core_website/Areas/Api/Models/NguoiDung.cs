using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using core_website.Models.CustomAttributes;

namespace core_website.Areas.Api.Models
{
  public class NguoiDung
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaND { get; set; }
    [Required(ErrorMessage = "{0} là bắt buộc!")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} tối thiểu 3 kí tự, tối đa 50 kí tự!")]
    [Display(Name = "Tên người dùng", Prompt = "Nhập tên người dùng")]
    public string? TenND { get; set; }
    [Required(ErrorMessage = "{0} là bắt buộc!")]
    [MatKhauManh]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu", Prompt = "Nhập mật khẩu")]
    public string? MatKhau { get; set; }
    [Required(ErrorMessage = "{0} là bắt buộc!")]
    [StringLength(20, ErrorMessage = "{0} không quá 20 kí tự!")]
    [RegularExpression("^(admin|user)$", ErrorMessage = "{0} phải là admin hoặc user!")]
    [Display(Name = "Vai trò", Prompt = "Chọn vai trò")]
    public string? VaiTro { get; set; } // 'admin' or 'user'
    [Required(ErrorMessage = "{0} là bắt buộc!")]
    [Display(Name = "Trại thái hoạt động")]
    public bool TrangThai { get; set; }
  }
  public class NguoiDungLoginRequest
  {
    public string? TenND { get; set; }
    public string? MatKhau { get; set; }
  }
  // Bỏ MatKhau, TrangThai 
  // NguoiDungResponse dùng để chứa thông tin người dùng (phục vụ cho các tác vụ cần đọc thông tin NguoiDung trừ những thông tin nhạy cảm)
  public class NguoiDungInfo
  {
    public int MaND { get; set; }
    public string? TenND { get; set; }
    public string? VaiTro { get; set; } // 'admin' or 'user'
  }
  public class NguoiDungResponse
  {
    public bool Success { get; set; }
    public string? Message { get; set; }
    public NguoiDungInfo User { get; set; }
  }
}
