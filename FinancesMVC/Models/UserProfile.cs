using System;
using System.Collections.Generic;

namespace FinancesMVC.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Country { get; set; }

    public string? AboutMe { get; set; }

    public virtual User? User { get; set; }
}
