using System.Collections.Generic;

namespace SFMForFraudTransactions.ViewModels
{
    public class TransactionIndexViewModel
    {
        public IEnumerable<PartialTransactions> Transactions { get; set; }
    }
}
