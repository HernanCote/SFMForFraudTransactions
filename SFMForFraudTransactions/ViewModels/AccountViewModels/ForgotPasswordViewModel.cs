using System.ComponentModel.DataAnnotations;

namespace SFMForFraudTransactions.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
