using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancesDomain.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    [Display(Name = "Money spent")]
    //add validation to pass decimals with , and .
    public decimal MoneySpent { get; set; }

    public DateTime? Date { get; set; }

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
