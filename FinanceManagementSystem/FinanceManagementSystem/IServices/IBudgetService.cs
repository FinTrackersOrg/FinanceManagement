using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.IServices
{
    public interface IBudgetService
    {
       Task<IEnumerable<Budget>> GetBudgetAsync();
       Task<Budget> GetBudgetAsyncById(int id);
       Task<Budget> PostBudgetAsync(BudgetDto budgetDto);

        Task<Budget> UpdateBudgetAsync(int id, BudgetDto budgetDto);
        Task<bool> DeleteBudgetAsync(int id);
    }
}
