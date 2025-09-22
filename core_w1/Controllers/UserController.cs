using core_w1.Services;
using Microsoft.AspNetCore.Mvc;
using core_w1.MiddleWares;
namespace core_w1.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRequestLogger _requestlogger;

        public UserController(IUserService userService, IRequestLogger requestLogger)
        {_requestlogger = requestLogger;
            _userService = userService;
        }
        public IActionResult UserList()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }
        public IActionResult Index()
        {
            var requestConn = HttpContext.Connection; // gets the ip address and port of the request
            var requestInfo = HttpContext.Request; //gets the request info url
            _requestlogger.Log($" [{DateTime.Now}] URL Request from {requestConn.RemoteIpAddress} to {requestInfo.Path}\n");
            var users = _userService.GetAllUsers();
            return Json(users);// return as json
        }
    }
}
