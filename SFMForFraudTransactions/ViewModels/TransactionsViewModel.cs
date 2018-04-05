using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFMForFraudTransactions.ViewModels
{
    public class TransactionsViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public string SearchTerm { get; set; }

        [Display(Name = "Is Fraud")]
        public bool IsFraud { get; set; }

        [Display(Name = "Destination Name")]
        public bool NameDest { get; set; }

        [Display(Name = "Transaction Date")]
        public bool TransactionDate { get; set; }
    }
}
