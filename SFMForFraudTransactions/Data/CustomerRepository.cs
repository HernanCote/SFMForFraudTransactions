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


        /// <summary>
        /// Create a customer in the database
        /// </summary>
        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        /// <summary>
        /// Return all customers in the database
        /// </summary>
        /// <returns>List of customers</returns>
        public IEnumerable<Customer> GetAllCustomers() => _context.Customers;

        /// <summary>
        /// Get a unique customer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id) => _context.Customers.FirstOrDefault(c => c.Id == id);

        /// <summary>
        /// Get a customer by name
        /// </summary>
        /// <param name="name">Name of the customer</param>
        /// <returns>Customer</returns>
        public Customer GetCustomerByName(string name)
        {
            return _context.Customers.FirstOrDefault(c => c.Name.Contains(name));
        }

        /// <summary>
        /// Save the changes made in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Update customers balance in the database
        /// </summary>
        /// <param name="originCustomer"></param>
        /// <param name="destinationCustomer"></param>
        /// <param name="amount"></param>
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
