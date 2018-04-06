using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFMForFraudTransactions.Data;
using SFMForFraudTransactions.Models;
using SFMForFraudTransactions.ViewModels;
using System;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Controllers.Api.Transactions
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : Controller
    {
        private ITransactionsRepository _transactRepository;
        private ICustomerRepository _customerRepository;
        private ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionsRepository transactRepository,
            ICustomerRepository customerRepository,
            ILogger<TransactionsController> logger)
        {
            _transactRepository = transactRepository;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_transactRepository.GetAllTranstactions());
        }


        [HttpGet("{id}", Name = "TransactionGet")]
        public IActionResult Get(int id)
        {
            try
            {
                var transaction = _transactRepository.GetTransactionById(id);
                if (transaction != null)
                {
                    return Ok(transaction);
                }
                else
                {
                    return NotFound("Transaction not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to retrieve transaction: {ex.Message}");
            }
            return BadRequest();
        }


        [HttpPost()]
        [Authorize(Roles = "Administrator, Assistant")]
        public async Task<IActionResult> Post([FromBody] CreateTransactionViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var originCustomer = _customerRepository.GetCustomerByName(viewModel.OriginCustomer);
                    var destinationCustomer = _customerRepository.GetCustomerByName(viewModel.DestinationCustomer);
                    if (originCustomer != null && destinationCustomer != null)
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
                            _customerRepository.UpdateCustomers(originCustomer, destinationCustomer, transaction.Amount);
                            if (await _customerRepository.SaveAsync())
                            {
                                var newUri = Url.Link("TransactionGet", new { id = transaction.Id });
                                return Created(newUri, transaction);
                            }
                        }
                    }
                    else
                    {
                        return NotFound("One or two of the customers does not exist in the database");
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save transaction: {ex.Message}");
            }

            return BadRequest("Information is invalid");
        }

        [Authorize(Roles = "Administrator, Assistant")]
        [HttpPost("search")]
        public IActionResult SearchTransaction([FromBody] SearchViewModel viewModel)
        {
            var searched = _transactRepository.GetAllTranstactions(viewModel.SearchTerm.ToLower());
            return Ok(searched);
        }
    }
}
