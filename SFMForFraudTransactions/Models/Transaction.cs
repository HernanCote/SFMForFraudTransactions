using System;

namespace SFMForFraudTransactions.Models
{   /// <summary>
    /// Transaction POCO
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public Customer OriginCustomer { get; set; }
        public Customer DestinationCustomer { get; set; }
        public int OldBalanceOrigin { get; set; }
        public int NewBalanceOrigin { get; set; }
        public int OldBalanceDestination { get; set; }
        public int NewBalanceDestination { get; set; }
        public int Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool IsFraud { get; set; }
        public bool IsFlaggedFraud { get; set; }
        public DateTime Date { get; set; }
    }
}
