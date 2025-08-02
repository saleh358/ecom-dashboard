using ECom_wep_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECom_wep_app.Controllers
{
    public class ProductController : Controller
    {
        // Product/List
        public IActionResult Lista()
        {
            ProductBL productBL = new ProductBL();
            List<Product> products = productBL.GetAllProducts();
            return View("Lista", products);
        }
        public IActionResult Info(int id)
        {
            ProductBL productBL = new ProductBL();
            Product product = productBL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("Info", product);

        }
    }
}
