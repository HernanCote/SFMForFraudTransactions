using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFMForFraudTransactions.Data;

namespace SFMForFraudTransactions.Controllers.Api.Transactions
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : Controller
    {
        private ITransactionsRepository _repository;
        private ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionsRepository repository, ILogger<TransactionsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        [Authorize(Roles = "Administrator, Assistant")]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllTranstactions());
        }
    }
}
