using Microsoft.AspNetCore.Mvc;

// KhoaTr - 5/10/2025: Sửa lại namespace từ core_w2 thành core_website
namespace core_website.Controllers
// KhoaTr - END
{
  [Area("Users")]
  public class ErrorController : Controller
  {
    public IActionResult HandleError(int statusCode)
    {
      switch (statusCode)
      {
        case 404: return View("404");
          //case ...?
      }
      return View("Error");
    }
  }
}
