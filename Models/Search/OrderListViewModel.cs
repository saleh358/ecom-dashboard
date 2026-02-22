using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;

namespace ECom_wep_app.Models.Search
{
    internal class OrderListViewModel
    {
        public OrderSearchModel Search { get; set; }
        public PaginatedList<Order> Orders { get; set; }
    }
}
