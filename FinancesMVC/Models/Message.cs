using System;
using System.Collections.Generic;

namespace FinancesMVC.Models;

public partial class Message
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
