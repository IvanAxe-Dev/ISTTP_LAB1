using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesMVC.Models;

public partial class Category
{
    public int Id { get; set; }

    [Display(Name="Category")]
    [Required(ErrorMessage ="This field should be filled.")]
    public string? Name { get; set; }

    public Guid? UserId { get; set; }

    [Display(Name = "Money spent")]
    [Required(ErrorMessage = "This field should be filled.")]
    [DataType(DataType.Currency)]
    public decimal TotalExpences { get => Transactions.Sum(x => x.MoneySpent); }

    [Display(Name = "Expenditure limit")]
    [Required(ErrorMessage = "This field should be filled.")]
    [DataType(DataType.Currency)]
    public double? ExpenditureLimit { get; set; }

    [Display(Name = "Parental control")]
    public bool IsParentControl { get; set; } = false;

    public virtual ICollection<SharedBudget> SharedBudgets { get; set; } = new List<SharedBudget>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}