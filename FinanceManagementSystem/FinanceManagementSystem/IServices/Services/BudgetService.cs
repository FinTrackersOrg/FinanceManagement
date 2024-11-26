using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagementSystem.IServices.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly FinanceDBContext _dbContext;
        private readonly ILogger<BudgetService> _logger;

        public BudgetService(FinanceDBContext dbContext, ILogger<BudgetService> logger)
        {
            _dbContext = dbContext;
           _logger = logger;
        }

        public async Task<IEnumerable<Budget>> GetBudgetAsync()
        {
            //_logger.LogInformation("Recevied a request to get details");
            return await _dbContext.Budgets.ToListAsync();
        }

        async Task<bool> IBudgetService.DeleteBudgetAsync(int id)
        {
            var budget = await _dbContext.Budgets.FindAsync(id);
            if (budget == null)
                return false;

            _dbContext.Budgets.Remove(budget);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        async Task<Budget> IBudgetService.GetBudgetAsyncById(int id)
        {
            return await _dbContext.Budgets.FindAsync(id);
        }

        async Task<Budget> IBudgetService.PostBudgetAsync(BudgetDto budgetDto)
        {
            Budget budget = new Budget();
            budget.BudgetId= budgetDto.BudgetId;
            budget.UserId = budgetDto.UserId;
            budget.Amount = budgetDto.Amount;
            budget.Category=budgetDto.Category;
            budget.CreatedDate = budgetDto.CreatedDate;

            await _dbContext.Budgets.AddAsync(budget);
            await _dbContext.SaveChangesAsync();
            return budget;
        }

        async Task<Budget> IBudgetService.UpdateBudgetAsync(int id, BudgetDto budgetDto)
        {
           
            var budget = await _dbContext.Budgets.FindAsync(id);
            if (budget== null)
                return null;


            budget.BudgetId = budgetDto.BudgetId;
            budget.UserId = budgetDto.UserId;
            budget.Amount = budgetDto.Amount;
            budget.Category = budgetDto.Category;
            budget.CreatedDate = budgetDto.CreatedDate;

            //_dbContext.Budgets.Update(budget);
            await _dbContext.SaveChangesAsync();
            return budget;
           



        }
    }
}
