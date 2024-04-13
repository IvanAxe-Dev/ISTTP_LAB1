using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinancesMVC.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Enter email or username:")]
        [UsernameOrEmailRequired]
        [Required(ErrorMessage = "Please enter either a valid username or email.")]
        public string UsernameOrEmail { get; set; }
    }
}