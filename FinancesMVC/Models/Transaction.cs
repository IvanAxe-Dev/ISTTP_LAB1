using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FinancesMVC.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }

    [Display(Name = "Money spent")]
    [MoneySpentValidation]
    [DataType(DataType.Currency)]
    public decimal MoneySpent { get; set; }

    [Display(Name = "When?")]
    public DateTime Date { get; set; }

    public bool? BudgetOverflown { get; set; }

    public int? MessageId { get; set; }

    public int? CompletedAchievementId { get; set; }

    public int? ExpenditureCategoryId { get; set; }

    [Display(Name = "Purpose")]
    public string? ExpenditureNote { get; set; }

    public virtual Achievement? CompletedAchievement { get; set; }

    public virtual Category? ExpenditureCategory { get; set; }

    public virtual Message? Message { get; set; }

    public virtual User? User { get; set; }
}

public class MoneySpentValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)  // If there's a value
        {
            // Apply your existing RegularExpression validation logic here
            var regex = new Regex(@"^[0-9]+(?:\,\d{1,3})*(?:\.[0-9]{2})?$");
            if (!regex.IsMatch(value.ToString()) || double.Parse(value.ToString()) <= 0)
            {   
                return new ValidationResult("Please enter a valid value (non-zero decimal only).");
            }
        }

        return ValidationResult.Success;
    }
}