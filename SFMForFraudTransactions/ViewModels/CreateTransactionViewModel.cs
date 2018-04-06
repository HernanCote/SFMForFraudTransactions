using SFMForFraudTransactions.Models;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// View Model for Creating New Transactions in the Web App
/// </summary>
namespace SFMForFraudTransactions.ViewModels
{
    public class CreateTransactionViewModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Origin Customer")]
        public string OriginCustomer { get; set; }

        [Required]
        [MinLength(4)]
        [Display(Name = "Destination Customer")]
        public string DestinationCustomer { get; set; }

        [Required]
        public int Amount { get; set; }

        [Display(Name = "Transaction Type")]
        [Required]
        public TransactionType Type { get; set; }

        [ValidDate]
        public string Date { get; set; }
    }
}
