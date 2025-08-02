using ECom_wep_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECom_wep_app.Controllers
{
    public class ProductController : Controller
    {
        // Product/List
        public IActionResult List()
        {
            ProductBL productBL = new ProductBL();
            List<Product> products = productBL.GetAllProducts();
            return View("List", products);
        }
        public IActionResult Details(int id)
        {
            ProductBL productBL = new ProductBL();
            Product product = productBL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("Details", product);

        }
    }
}
