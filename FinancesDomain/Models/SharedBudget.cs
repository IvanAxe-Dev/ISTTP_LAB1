using System;
using System.Collections.Generic;

namespace FinancesDomain.Models;

public partial class SharedBudget
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int AddedUserId { get; set; }

    public int CommonCategoryId { get; set; }

    public virtual Category CommonCategory { get; set; } = null!;

    public virtual User AddedUser { get; set; } = null!;
}
