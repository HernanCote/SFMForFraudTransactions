using System.ComponentModel.DataAnnotations;

namespace SFMForFraudTransactions.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
