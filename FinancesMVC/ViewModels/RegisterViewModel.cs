using System.ComponentModel.DataAnnotations;

namespace FinancesMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [Display(Name = "Enter a username:")]
        [StringLength(50, ErrorMessage = "Username must be at least {2} characters long.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        [Display(Name = "Enter your email:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Select the year you were born:")]
        [MinimumAge(8)]
        public int BirthYear { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [Display(Name = "Enter password:")]
        [StringLength(20, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm the password.")]
        [Display(Name = "Repeat password:")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
            ErrorMessage = "You should be at least " + _minimumAge + " years old to use the app.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            int birthYear;
            if (int.TryParse(value.ToString(), out birthYear))
            {
                int currentYear = DateTime.Now.Year;
                return birthYear <= currentYear - _minimumAge;
            }

            return false;
        }
    }
}
