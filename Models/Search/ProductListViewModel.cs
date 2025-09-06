using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;

namespace ECom_wep_app.Models.Search
{
    internal class ProductListViewModel
    {
        public ProductSearchModel Search { get; set; }
        public PaginatedList<Product> Products { get; set; }
    }
}