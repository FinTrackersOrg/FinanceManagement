using System.ComponentModel.DataAnnotations;

namespace FinanceManagementSystem.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }

        public virtual User User { get; set; }
    }
}
