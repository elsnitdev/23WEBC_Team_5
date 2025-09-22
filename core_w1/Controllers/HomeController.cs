using System.Diagnostics;
using core_w1.Models;
using Microsoft.AspNetCore.Mvc;
using core_w1.MiddleWares;
namespace core_w1.Controllers
{
    public class HomeController : Controller
    {
        // readonly used to assign value only in constructor
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestLogger _requestlogger;
        public HomeController(ILogger<HomeController> logger,IRequestLogger requestLogger)
        {
            _logger = logger;
            _requestlogger = requestLogger;
        }

        public IActionResult Index()
        {
            var requestConn = HttpContext.Connection; // gets the ip address and port of the request
            var requestInfo = HttpContext.Request; //gets the request info url
            _requestlogger.Log( $" [{DateTime.Now}] URL Request from {requestConn.RemoteIpAddress} to {requestInfo.Path}\n");
            return View();
        }

        public IActionResult Privacy()
        {
            var requestConn = HttpContext.Connection; // gets the ip address and port of the request
            var requestInfo = HttpContext.Request; //gets the request info url
            _requestlogger.Log($" [{DateTime.Now}] URL Request from :{requestConn.RemoteIpAddress} :to {requestInfo.Path}\n");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
