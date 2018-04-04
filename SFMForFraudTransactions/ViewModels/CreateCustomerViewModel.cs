using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
