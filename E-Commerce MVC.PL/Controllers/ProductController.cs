using E_Commerce.Services.Interfaces;
using E_Commerce.Services.Model_View;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: /Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await _productService.GetAllBrandsAsync();
            ViewBag.Types = await _productService.GetAllTypesAsync();
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await _productService.GetAllBrandsAsync();
                ViewBag.Types = await _productService.GetAllTypesAsync();
                return View(model);
            }

            await _productService.AddProductAsync(model);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var viewModel = new UpdateProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity
            };

            return View(viewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //@Html.AntiForgeryToken() ابقا ضيف السطر ده فى ال view
        public async Task<IActionResult> Edit(UpdateProductVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.UpdateProductAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
