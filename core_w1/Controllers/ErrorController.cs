using Microsoft.AspNetCore.Mvc;

namespace core_w1.Controllers
{
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
