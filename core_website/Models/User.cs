// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.Models
// KhoaTr - END
{
  public class User
  {
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public int RoleId { get; set; }

  }
}
