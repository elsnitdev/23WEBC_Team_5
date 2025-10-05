using Microsoft.AspNetCore.Mvc;

namespace core_website.Areas.Admins.Controllers
{
    [Area("Admins")]
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
