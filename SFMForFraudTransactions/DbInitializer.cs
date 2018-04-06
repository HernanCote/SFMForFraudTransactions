using SFMForFraudTransactions.Data;
using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMForFraudTransactions
{
    public class DbInitializer
    {
        private ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(initialCustomers);
                await _context.SaveChangesAsync();
            }
        }

        List<Customer> initialCustomers = new List<Customer>
        {
            new Customer()
            {
                Name = "C1231006815",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C1666544295",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C1305486145",
                Balance = 100000
            }
            ,new Customer()
            {
                Name = "C840083671",
                Balance = 100000
            }
            ,new Customer()
            {
                Name = "C1912850431",
                Balance = 100000
            }
            ,new Customer()
            {
                Name = "C712410124",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C1043358826",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C1053967012",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C1632497828",
                Balance = 100000
            },
            new Customer()
            {
                Name = "C783286238",
                Balance = 100000
            },
            new Customer()
            {
                Name = "M396485834",
                Balance = 100000
            },
            new Customer()
            {
                Name = "M1426725223",
                Balance = 100000
            },
            new Customer()
            {
                Name = "M1971783162",
                Balance = 100000
            }
        };
    }
}
