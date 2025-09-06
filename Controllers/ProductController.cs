using ECom_wep_app.Models;
using ECom_wep_app.Repository.Abstract;
using ECom_wep_app.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECom_wep_app.Controllers
{
    public class ProductController :Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
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
                await _productRepository.AddProductAsync(product);
                return RedirectToAction("List");
            }
            return View("Create", product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateProduct(Product product)
        //{
        //    if (!ModelState.IsValid)
        //        return View("Update", product);

        //    var existing = await _productRepository.GetProductByIdAsync(product.Id);
        //    if (existing == null)
        //        return NotFound();

            
        //    existing.Name = product.Name;
        //    existing.ImageUrl = product.ImageUrl;

        //    await _productRepository.UpdateProductAsync(existing);
        //    return RedirectToAction(nameof(List));
        //}

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return View(product);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("List");
        }
    }
}
