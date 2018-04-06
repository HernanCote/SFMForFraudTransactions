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

        /// <summary>
        /// Return Index View of the transactions controller, if a query string is passed, then search by the search term.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return Create View of the Transaction Controller, Only Administrator and Assistant roles
        /// can access this feature
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator, Assistant")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a transaction, only administrator and assistant roles can access this feature
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Assistant")]
        public async Task<IActionResult> Create(CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var originCustomer = _customerRepository.GetCustomerByName(viewModel.OriginCustomer);
                var destinationCustomer = _customerRepository.GetCustomerByName(viewModel.DestinationCustomer);

                //Check if Origin Customer and Destination Customer Exists in the database
                if ((originCustomer != null) && (destinationCustomer != null))
                {
                    ///Create the model
                    var transaction = new Transaction
                    {
                        OriginCustomer = originCustomer,
                        DestinationCustomer = destinationCustomer,
                        Amount = viewModel.Amount,
                        Date = DateTime.Parse(viewModel.Date),
                        TransactionType = viewModel.Type
                    };

                    //Save the transaction in the database
                    _transactRepository.SaveTransaction(transaction);

                    //Apply changes to the database
                    if (await _transactRepository.SaveAsync())
                    {
                        //Update customers balance
                        _customerRepository.UpdateCustomers(originCustomer, destinationCustomer, transaction.Amount);
                        if (await _customerRepository.SaveAsync())
                        {
                            //Redirect to Index view of the transaction controller
                            return RedirectToAction(nameof(Index));
                        }
                    }

                }
            }
            //Return same view to display validation errors
            return View();
        }

        /// <summary>
        /// Access Edit page.
        /// Only Manager and Administrator roles can access this feature
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public IActionResult Edit(int id)
        {
            var transaction = _transactRepository.GetTransactionById(id);
            return View(transaction);
        }

        /// <summary>
        /// Edit a transaction, only IsFraud and IsFlaggedFraud can be updated.
        /// Only Manager and Administrator roles can access this feature
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Search for a particular transaction given a serch term.
        /// Only Administrator and Assistant roles can access this feature
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Assistant")]
        public IActionResult Search(TransactionsViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm.ToLower() });
        }

    }
}
