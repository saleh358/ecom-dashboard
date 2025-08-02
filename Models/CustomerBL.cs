namespace ECom_wep_app.Models;

public class CustomerBL
{
    private List<Customer> CustomerList; // Declare CustomerList as a private field

    public CustomerBL()
    {
        CustomerList = new List<Customer> // Initialize CustomerList in the constructor
        {
            new Customer { Id = 1, Name = "Ali", Email = "Ali@gmail.com", PhoneNumber = "1234567890", Address = "123 Main St" ,ImageUrl = "1.jpg"},
            new Customer { Id = 2, Name = "Sara", Email = "Sara@gmail.com", PhoneNumber = "0987654321", Address = "456 Elm St" ,ImageUrl = "2.png"},
            new Customer { Id = 3, Name = "ahmed", Email = "ahmed@gmail.com", PhoneNumber = "0987654321", Address = "456 Elm St" ,ImageUrl = "1.jpg"},
            new Customer { Id = 4, Name = "sayed", Email = "sayed@gmail.com", PhoneNumber = "0987654321", Address = "456 Elm St" ,ImageUrl = "1.jpg"},
            new Customer { Id = 5, Name = "mona", Email = "mona@gmail.com", PhoneNumber = "0987654321", Address = "456 Elm St",ImageUrl = "2.png" }
        };
    }

    // Method to get all customers
    public List<Customer> GetAllCustomers()
    {
        return CustomerList; // Access the private field
    }
    public Customer GetCustomerById(int id)
    {
        return CustomerList.FirstOrDefault(c => c.Id == id);
    }
}