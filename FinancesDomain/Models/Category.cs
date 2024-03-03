using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinancesDomain.Models;

public partial class Category
{
    public int Id { get; set; }

    [Display(Name="Category")]
    [Required(ErrorMessage ="This field should be filled.")]
    public string? Name { get; set; }

    public int? UserId { get; set; }

    [Display(Name = "Money spent")]
    [Required(ErrorMessage = "This field should be filled.")]
    public decimal TotalExpences { get; set; } = 0;

    public string? CategoryColorHexCode { get; set; }

    [Display(Name = "Expenditure limit")]
    [Required(ErrorMessage = "This field should be filled.")]
    public double? ExpenditureLimit { get; set; }

    [Display(Name = "Parental control")]
    public bool IsParentControl { get; set; } = false;

    public virtual ICollection<SharedBudget> SharedBudgets { get; set; } = new List<SharedBudget>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
