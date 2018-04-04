using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        Task<bool> SaveAsync();
    }
}
