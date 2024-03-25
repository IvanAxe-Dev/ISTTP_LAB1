using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinancesMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Enter email or username:")]
        [UsernameOrEmailRequired]
        [Required(ErrorMessage = "Please enter either a valid username or email.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage ="Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password:")]
        public string Password { get; set; }

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}

public class UsernameOrEmailRequiredAttribute : ValidationAttribute
{
    private const string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    private const string usernamePattern = @"^[a-zA-Z0-9_]+$";

    public new string ErrorMessage { get; set; } = "Please provide either a valid Username or Email.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var input = value as string;

        if (string.IsNullOrEmpty(input))
        {
            return new ValidationResult(ErrorMessage);
        }

        // Prioritize email validation (adjust this if needed)
        if (Regex.IsMatch(input, emailPattern))
        {
            return ValidationResult.Success;
        }

        if (Regex.IsMatch(input, usernamePattern))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}