using System;
using System.Collections.Generic;

namespace FinancesMVC.Models;

public partial class Achievement
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
