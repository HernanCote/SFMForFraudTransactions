using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// View Model to create new Customers
/// </summary>
namespace SFMForFraudTransactions.ViewModels
{
    public class CreateCustomerViewModel
    {
        [Required]
        [DisplayName("Customer Name")]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Initial Balance")]
        public int InitialBalance { get; set; }
    }
}
