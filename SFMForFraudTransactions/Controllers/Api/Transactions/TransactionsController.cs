using Microsoft.AspNetCore.Mvc;
using SFMForFraudTransactions.Data;

namespace SFMForFraudTransactions.Controllers.Api.Transactions
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private ITransactionsRepository _repository;

        public TransactionsController(ITransactionsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllTranstactions());
        }
    }
}
