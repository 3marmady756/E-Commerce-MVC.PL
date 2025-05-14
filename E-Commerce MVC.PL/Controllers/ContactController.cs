using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.PL.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View(); // This will look for Views/Contact/Contact.cshtml
        }
    }
} 