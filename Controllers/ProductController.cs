using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECom_wep_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("Details", product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction("List");
            }
            return View("Create", product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View("Update", product);

            var existing = await _productService.GetProductByIdAsync(product.Id);
            if (existing == null)
                return NotFound();

            existing.Name = product.Name;
            existing.ImageUrl = product.ImageUrl;

            await _productService.UpdateProductAsync(existing);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(
            ProductSearchModel search,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = await _productService.ProductSearchAsync(search);

            var paged = await PaginatedList<Product>.CreateAsync(
                query.AsNoTracking(),
                page,
                pageSize
            );
            var vm = new ProductListViewModel { Search = search, Products = paged };
            return View(vm);
        }
    }
}
