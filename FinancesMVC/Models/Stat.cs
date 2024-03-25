using System;
using System.Collections.Generic;

namespace FinancesMVC.Models;

public partial class Stat
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public int? ChosenCategoryId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public double? CalculatedExpances { get; set; }

    public virtual Category? ChosenCategory { get; set; }

    public virtual User? User { get; set; }
}
