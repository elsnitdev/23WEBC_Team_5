using core_website.Models;
namespace core_website.Services
{
  public interface IUserService // create the interface for user service
  {
    List<User> GetAllUsers();
    // KhoaTr - 22/09/2025: Thêm phương thức overload GetAllUsers với kiểu trả về UserResponse để hỗ trợ phân trang
    UserResponse GetAllUsers(int skip, int limit);
    void setUsers(List<User> users);

  }
}
