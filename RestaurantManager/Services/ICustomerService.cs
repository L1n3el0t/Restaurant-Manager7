using RestaurantManager.Models;

namespace RestaurantManager.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task AddCustomer(Customer customerName);
        Task EditCustomer(Customer customer);
        Task DeleteCustomer(int id);




    }
}
