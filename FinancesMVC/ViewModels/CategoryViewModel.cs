using FinancesMVC.Models;

namespace FinancesMVC.ViewModels
{
    public class CategoryViewModel
    {
        public virtual Category Category { get; set; }

        public virtual List<string>? SelectedUsers { get; set; }
    }
}
