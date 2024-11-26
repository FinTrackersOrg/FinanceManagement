using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.IServices
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetIncomeAsync();
        Task<Income> GetIncomeAsyncById(int id);
        Task<Income> PostIncomeAsync(IncomeDto incomeDto);
        Task<Income> UpdateIncomeAsync(int id,IncomeDto incomeDto);
       Task<bool> DeleteIncomeAsync(int id);

        List<string> GetCategories();



    }
}
