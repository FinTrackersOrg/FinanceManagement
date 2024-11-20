using FinanceManagementSystem.DTO;
using FinanceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagementSystem.IServices.Services
{
    public class ExpenseService:IExpenseService
    {
        private readonly FinanceDBContext _dbContext;
        public ExpenseService(FinanceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

     

       async Task<Expense> IExpenseService.AddExpense(ExpenseDto expenseDto)
        {
            Expense expense = new Expense();
            expense.ExpenseId = expenseDto.ExpenseId;
            expense.UserId = expenseDto.UserId;
            expense.Amount = expenseDto.Amount;
            expense.Category = expenseDto.Category;
            expense.ExpenseDate = expenseDto.ExpenseDate;

            await _dbContext.Expenses.AddAsync(expense);
            await _dbContext.SaveChangesAsync();
            return expense;

        }

       async Task<bool> IExpenseService.DeleteExpense(int id)
        {
            var expense = await _dbContext.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        
        async Task<IEnumerable<Expense>> IExpenseService.GetExpense()
        {
            return await _dbContext.Expenses.ToListAsync();
        }

        async Task<Expense> IExpenseService.GetExpenseById(int id)
        {
            return await _dbContext.Expenses.FindAsync(id);
        }

        async Task<Expense> IExpenseService.UpdateExpense(int id, ExpenseDto expenseDto)
        {
            var expense = await _dbContext.Expenses.FindAsync(id);
            if (expense == null)
                return null;
           
            expense.ExpenseId = expenseDto.ExpenseId;
            expense.UserId = expenseDto.UserId;
            expense.Amount = expenseDto.Amount;
            expense.Category = expenseDto.Category;
            expense.ExpenseDate = expenseDto.ExpenseDate;
            //_dbContext.Expenses.Update(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }


    }

      
    }

