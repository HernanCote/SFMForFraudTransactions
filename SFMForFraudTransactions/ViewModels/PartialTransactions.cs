using SFMForFraudTransactions.Models;
using System;

namespace SFMForFraudTransactions.ViewModels
{
    public class PartialTransactions
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public bool IsFraud { get; set; }
        public bool IsFlaggedFraud { get; set; }
        public DateTime Date { get; set; }
    }
}
