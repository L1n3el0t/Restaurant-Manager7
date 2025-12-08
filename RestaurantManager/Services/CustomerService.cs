using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data;
using RestaurantManager.Models;
using ServiceStack.Script;
using System;

namespace RestaurantManager.Services
{
    public class CustomerService : ICustomerService
    {
        // Implementation of customer-related business logic will go here in the future.

        private readonly ILogger<CustomerService> _logger;
        private readonly RestaurantManagerContext _context;


        public CustomerService( RestaurantManagerContext context , ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(GetAllCustomers);
           
            var customers = await _context.Customer.ToListAsync();
            _logger.LogInformation($"All customers retrieved using {action} by request {correlationId} on {DateTime.UtcNow}.");
            return customers;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(GetCustomerById);
            var customers = await _context.Customer.FromSqlRaw<Customer>("spGetCustomerById {0}", id).ToListAsync();
            try {
                
                var customer = customers.FirstOrDefault();

                if (customer != null)
                {
                    _logger.LogInformation($"Customer with ID {id} retrieved using {action} by request {correlationId} on {DateTime.UtcNow}.");
                }
                else
                {
                    _logger.LogWarning($"Customer with ID {id} retrieved using {action} not found from request {correlationId} on {DateTime.UtcNow}.");
                }
                return customer;
            } catch (Exception ex)
            {
                
                _logger.LogError($"Error retrieving customer with ID {id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }





        public async Task AddCustomer(Customer customer)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(AddCustomer);
            try {

                _context.Customer.Add(customer);
                _logger.LogInformation($"Customer {customer.Name} added using {action} by request {correlationId} on {DateTime.UtcNow}.");
                await _context.SaveChangesAsync();
                
            } catch (Exception ex)
            {
                _logger.LogError($"Error adding customer {customer.Name} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }


        }
        public async Task EditCustomer(Customer customer)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(EditCustomer);

            try
            {
                _context.Customer.Update(customer);
                _logger.LogInformation($"Customer with ID {customer.Id} updated using {action} by request {correlationId} on {DateTime.UtcNow}.");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating customer with ID {customer.Id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
            }
        public async Task DeleteCustomer(int id)
        {
            var correlationId = Guid.NewGuid().ToString();
            var action = nameof(DeleteCustomer);
            
            try {
                var customer = await _context.Customer.FindAsync(id);
                if (customer != null)
                {
                    _context.Customer.Remove(customer);
                    _logger.LogInformation($"Customer with ID {id} deleted using {action} by request {correlationId} on {DateTime.UtcNow}.");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning($"Customer with ID {id} not found for deletion using {action} by request {correlationId} on {DateTime.UtcNow}.");
                }
                } catch (Exception ex)
            {
                _logger.LogError($"Error deleting customer with ID {id} using {action} by request {correlationId} on {DateTime.UtcNow}: {ex.Message}");
                throw;
            }
        }


    }
}