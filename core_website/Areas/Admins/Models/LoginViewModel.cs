using System.ComponentModel.DataAnnotations;

namespace core_website.Areas.Admins.Models
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    public string TenND { get; set; }

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string MatKhau { get; set; }
  }
}
