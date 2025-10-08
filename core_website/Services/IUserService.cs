// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
using core_website.Models;
namespace core_website.Services
// KhoaTr - END
{
  public interface IUserService //Tinle- create the interface for user service
    {
    List<User> GetAllUsers();
    // KhoaTr - 22/09/2025: Thêm phương thức overload GetAllUsers với kiểu trả về UserResponse để hỗ trợ phân trang
    UserResponse GetAllUsers(int skip, int limit);
    void setUsers(List<User> users);

  }
}
