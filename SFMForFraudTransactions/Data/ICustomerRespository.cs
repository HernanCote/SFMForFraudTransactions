using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    /// <summary>
    /// Customer Repository interface
    /// </summary>
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        Customer GetCustomerByName(string name);
        Task<bool> SaveAsync();
        void UpdateCustomers(Customer originCustomer, Customer destinationCustomer, int amount);
    }
}
