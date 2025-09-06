using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;

namespace ECom_wep_app.Models.Search
{
    internal class CustomerListViewModel
    {
        public CustomerSearchModel Search { get; set; }
        public PaginatedList<Customer> Customers { get; set; }
    }
}