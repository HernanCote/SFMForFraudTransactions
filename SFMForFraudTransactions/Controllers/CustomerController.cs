using Microsoft.AspNetCore.Mvc;
using SFMForFraudTransactions.Data;
using SFMForFraudTransactions.Models;
using SFMForFraudTransactions.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customers = _repository.GetAllCustomers().ToList();
            return View(customers);
        }

        public IActionResult Detail(int id)
        {
            var customer = _repository.GetCustomer(id);
            if (customer != null)
            {
                return View(customer);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    NewBalance = model.InitialBalance,
                    OldBalance = model.InitialBalance
                };

                _repository.CreateCustomer(customer);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Detail), new { id = customer.Id });
            }

            return View();
        }
    }
}
