using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinancesMVC.Models;

public partial class User : IdentityUser<Guid>
{
    public override Guid Id { get => base.Id; set => base.Id = value; }

    [Required]
    [StringLength(100)]
    [MaxLength(100)]
    [NoCurseWords]
    public override string? UserName { get => base.UserName; set => base.UserName = value; }

    [Required]
    [EmailAddress]
    public override string? Email { get => base.Email; set => base.Email = value; }

    [Required]
    public int? BirthYear { get; set; }

    public bool IsMature { get; set; } = false;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<SharedBudget> SharedBudgets { get; set; } = new List<SharedBudget>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserProfile? UserProfile { get; set; }
}

public class NoCurseWordsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string userName = value.ToString()!;
            string curseWordsPattern = @"\b(ass|bitch|crap|damn|fuck|hell|piss|shit|bastard|cunt|dick|nigga|nigger|suck)\b";

            if (Regex.IsMatch(userName, curseWordsPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Username cannot contain curse words.");
            }
        }

        return ValidationResult.Success;
    }
}