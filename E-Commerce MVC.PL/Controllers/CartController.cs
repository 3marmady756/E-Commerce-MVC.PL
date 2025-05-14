using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.PL.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 