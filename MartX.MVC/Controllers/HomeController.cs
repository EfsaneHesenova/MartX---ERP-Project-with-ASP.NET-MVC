using MartX.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    [Authorize(Roles = "Admin, Boss, Adminstrator, Worker")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
