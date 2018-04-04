using System;

namespace SFMForFraudTransactions.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Customer OriginCustomer { get; set; }
        public Customer DestinationCustomer { get; set; }
        public int Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool IsFraud { get; set; }
        public bool IsFlaggedFraud { get; set; }
        public DateTime Date { get; set; }
    }
}
