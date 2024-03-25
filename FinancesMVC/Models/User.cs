using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinancesMVC.Models;

public partial class User : IdentityUser<Guid>
{
    public override Guid Id { get => base.Id; set => base.Id = value; }

    [Required]
    [StringLength(100)]
    [MaxLength(100)]
    public override string? UserName { get => base.UserName; set => base.UserName = value; }
    [Required]
    [EmailAddress]
    public override string? Email { get => base.Email; set => base.Email = value; }

    [Required]
    public int? BirthYear { get; set; }

    public bool? IsMature { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<SharedBudget> SharedBudgets { get; set; } = new List<SharedBudget>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserProfile? UserProfile { get; set; }
}
