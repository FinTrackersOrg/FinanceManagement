namespace FinanceManagementSystem.DTO
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
