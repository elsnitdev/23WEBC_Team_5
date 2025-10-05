// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.MiddleWares
// KhoaTr - END
{
  public class UserLoadingListUser
  {
    private readonly RequestDelegate _next;
    public UserLoadingListUser(RequestDelegate next)
    {
      _next = next;
    }

  }
}
