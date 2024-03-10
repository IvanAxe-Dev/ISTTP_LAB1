using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancesDomain.Models;

public partial class SharedBudget
{
    public int Id { get; set; }

    [Display(Name = "Budget")]
    public string Title { get; set; }

    [Display(Name = "Members")]
    public int AddedUserId { get; set; }

    [Display(Name = "Spent | Limit")]
    public int CommonCategoryId { get; set; }

    [Display(Name = "Category")]
    public virtual Category CommonCategory { get; set; } = null!;

    public virtual User AddedUser { get; set; } = null!;
}
