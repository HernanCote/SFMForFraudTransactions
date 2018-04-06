using SFMForFraudTransactions.Models;
using System.Collections.Generic;

/// <summary>
/// Transaction View Model to display the Transactions Index view
/// </summary>
namespace SFMForFraudTransactions.ViewModels
{
    public class TransactionsViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public string SearchTerm { get; set; }
    }
}
