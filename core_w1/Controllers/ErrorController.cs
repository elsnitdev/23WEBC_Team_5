using Microsoft.AspNetCore.Mvc;

namespace core_w1.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
          return View("~/Views/Shared/Error.cshtml");
        }

        [Route("Error/404")]
        public IActionResult NotFound()
        {
          Response.StatusCode = 404; // Ensure the response status is 404
          return View("~/Views/Shared/404.cshtml");
        }
  }
}
