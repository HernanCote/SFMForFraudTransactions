using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMForFraudTransactions.Data;
using SFMForFraudTransactions.Models;
using SFMForFraudTransactions.ViewModels;
using System;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ITransactionsRepository _transactRepository;
        private ICustomerRepository _customerRepository;

        public TransactionsController(ITransactionsRepository transactRepository, ICustomerRepository customerRepository)
        {
            _transactRepository = transactRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Index(string query = null)
        {
            var transactions = _transactRepository.GetAllTranstactions(query);
            var transactionsViewModel = new TransactionsViewModel
            {
                Transactions = transactions
            };

            return View(transactionsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Assistant")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Assistant")]
        public async Task<IActionResult> Create(CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var originCustomer = _customerRepository.GetCustomerByName(viewModel.OriginCustomer);
                var destinationCustomer = _customerRepository.GetCustomerByName(viewModel.DestinationCustomer);

                if ((originCustomer != null) && (destinationCustomer != null))
                {
                    var transaction = new Transaction
                    {
                        OriginCustomer = originCustomer,
                        DestinationCustomer = destinationCustomer,
                        Amount = viewModel.Amount,
                        Date = DateTime.Parse(viewModel.Date),
                        TransactionType = viewModel.Type
                    };

                    _transactRepository.SaveTransaction(transaction);
                    if (await _transactRepository.SaveAsync())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View();
                    }

                }
                return View();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public IActionResult Edit(int id)
        {
            var transaction = _transactRepository.GetTransactionById(id);
            return View(transaction);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var updatedTransaction = _transactRepository.UpdateTransaction(transaction);
                if (updatedTransaction != null)
                {
                    if (await _transactRepository.SaveAsync())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        [Authorize(Roles = "Administrator, Assistant")]
        [HttpPost]
        public IActionResult Search(TransactionsViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm.ToLower() });
        }

    }
}
