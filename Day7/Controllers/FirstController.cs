using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index1() => View();
        public IActionResult Index2() => View();
        public IActionResult Index3() => View();
    }
}
