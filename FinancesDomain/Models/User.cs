using System;
using System.Collections.Generic;

namespace FinancesDomain.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public decimal? AccountMoney { get; set; }

    public bool? IsMature { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<SharedBudget> SharedBudgets { get; set; } = new List<SharedBudget>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserProfile? UserProfile { get; set; }
}
