namespace ECom_wep_app.Models
{
    public class ProductBL
    {
        private List<Product> ProductList;
        public ProductBL()
        {
            ProductList = new List<Product> // Initialize CustomerList in the constructor
             {
               new Product { Id = 1, Name = "Creed Aventus", Type = "Classic",ImageUrl = "aventus.jpeg"},
               new Product { Id = 2, Name = "Tygar", Type = "Citrus",ImageUrl = "tygar.jpeg"},
               new Product { Id = 3, Name = "Interload", Type = "Ensence",ImageUrl = "inter.jpeg"}

             };
        }


        public List<Product> GetAllProducts()
        {
            return ProductList; // Access the private field
        }
        public Product GetProductById(int id)
        {
            return ProductList.FirstOrDefault(c => c.Id == id);
        }

    }
}
