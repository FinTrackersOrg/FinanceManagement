namespace FinanceManagementSystem.DTO
{
    public class IncomeDto
    {
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
    }
}
