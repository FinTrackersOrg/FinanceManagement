using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.IServices
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpense();
        Task<Expense> GetExpenseById(int id);
        Task<Expense> AddExpense(ExpenseDto expenseDto);
        Task<Expense> UpdateExpense(int id, ExpenseDto expenseDto);
        Task<bool> DeleteExpense(int id);

    }
}
