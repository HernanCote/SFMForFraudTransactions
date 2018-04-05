using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public IEnumerable<Customer> GetAllCustomers() => _context.Customers;


        public Customer GetCustomer(int id) => _context.Customers.FirstOrDefault(c => c.Id == id);

        public Customer GetCustomerByName(string name)
        {
            return _context.Customers.FirstOrDefault(c => c.Name.Contains(name));
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void UpdateCustomers(Customer originCustomer, Customer destinationCustomer, int amount)
        {
            var origin = GetCustomerByName(originCustomer.Name);
            var dest = GetCustomerByName(destinationCustomer.Name);
            if (origin != null && dest != null)
            {
                origin.Balance -= amount;
                dest.Balance += amount;
                _context.Customers.Update(origin);
                _context.Customers.Update(dest);
            }
        }
    }
}
