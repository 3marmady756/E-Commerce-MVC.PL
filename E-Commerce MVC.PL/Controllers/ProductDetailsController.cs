using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.PL.Controllers
{
    public class ProductDetailsController : Controller
    {
        public IActionResult Details(int id)
        {
            // In a real app, fetch product by id
            return View();
        }
    }
} 