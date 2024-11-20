using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FinanceManagementSystem.IServices.Services
{
    public class IncomeService :IIncomeService
    {
       private readonly FinanceDBContext _dbContext;

       public IncomeService(FinanceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteIncomeAsync(int id)
        {
            var income = await _dbContext.Incomes.FindAsync(id);
            if (income == null)
                return false;

            _dbContext.Incomes.Remove(income);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    

        public async Task<IEnumerable<Income>> GetIncomeAsync()
        {
            return (IEnumerable<Income>)await _dbContext.Incomes.ToListAsync();
            
        }

        public async Task<Income> GetIncomeAsyncById(int id)
        {
            return await _dbContext.Incomes.FindAsync(id);
        }

        public async Task<Income> PostIncomeAsync(IncomeDto incomeDto)
        {
            Income income=new Income();
            income.IncomeId = incomeDto.IncomeId;
            income.UserId = incomeDto.UserId;
            income.Source = incomeDto.Source;
            income.IncomeDate = incomeDto.IncomeDate;
            income.Amount = incomeDto.Amount;
            await _dbContext.Incomes.AddAsync(income);
            await _dbContext.SaveChangesAsync();
            return income;
        }

        public async Task<Income> UpdateIncomeAsync(int id, IncomeDto incomeDto)
        {
            var income = await _dbContext.Incomes.FindAsync(id);
            if (income == null)
                return null;

            income.UserId = incomeDto.UserId;
            income.Source = incomeDto.Source;
            income.Amount = incomeDto.Amount;
            income.IncomeDate = incomeDto.IncomeDate;

            //_dbContext.Incomes.Update(existingIncome);
            await _dbContext.SaveChangesAsync();
            return income;
        }



    }
}
