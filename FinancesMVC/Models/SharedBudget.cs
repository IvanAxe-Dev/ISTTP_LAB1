using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancesMVC.Models;

public partial class SharedBudget
{
    public int Id { get; set; }

    public Guid OwnerId { get; set; }

    [Display(Name = "Members")]
    public List<Guid>? AddedUsersId { get; set; }

    [Display(Name = "Spent | Limit")]
    public int CommonCategoryId { get; set; }

    [Display(Name = "Category")]
    public virtual Category CommonCategory { get; set; } = null!;

    public virtual User OwnerUser { get; set; } = null!;
}
