using FinanceManagementSystem.DTO;
using FinanceManagementSystem.IServices;
using FinanceManagementSystem.IServices.Services;
using FinanceManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly ILogger<ExpenseController> _logger;
        public ExpenseController(IExpenseService expenseService, ILogger<ExpenseController> logger)
        {
            _expenseService = expenseService;
            _logger = logger;
        }

        /// <summary>
        /// Get all the records of Expense Table
        /// </summary>
        /// <returns>List of records</returns>
        
        [HttpGet]
        public async Task<IEnumerable<Expense>> GetExpense()
        {
            _logger.LogInformation("Received a Get Request");
            return await _expenseService.GetExpense();
        }

        /// <summary>
        /// Get perticular record based on Id
        /// </summary>
        /// <param name="id">Expense Id</param>
        /// <returns>Single record corresponding to Id</returns>
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            _logger.LogInformation("Recevied a Get Request By Id");
            var expense = await _expenseService.GetExpenseById(id);
            if (expense== null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(expense);
        }
        /// <summary>
        /// Add new Records to the Expense Table
        /// </summary>
        /// <param name="expenseDto">Expense Dto object</param>
        /// <returns>Newly added record </returns>
        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseDto expenseDto)
        {
            _logger.LogInformation("Received a Add Request");
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var createdIncome = await _expenseService.AddExpense(expenseDto);
            return CreatedAtAction(nameof(GetExpenseById), new { id = createdIncome.ExpenseId }, createdIncome);
        }
        /// <summary>
        /// Update the records based on Id
        /// </summary>
        /// <param name="id">Expense ID</param>
        /// <param name="expenseDto">Expense DTO object</param>
        /// <returns>Newly updated record</returns>
        [HttpPut]
        public async Task<IActionResult> UpdatedExpense(int id, ExpenseDto expenseDto)
        {
            _logger.LogInformation("Received a Update Request");
            if (id != expenseDto.ExpenseId)
                return BadRequest(new { Message = "ID mismatch between the route and payload." });

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var isUpdated = await _expenseService.UpdateExpense(id, expenseDto);
            if (isUpdated == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(isUpdated);
        }

        /// <summary>
        /// Delete the Expense based on Id
        /// </summary>
        /// <param name="id">Expense Id</param>
        /// <returns>Remaning records after deletion</returns>

        [HttpDelete]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            _logger.LogInformation("Received a Delete Request");
            var isDeleted = await _expenseService.DeleteExpense(id);
            if (!isDeleted)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return NoContent();
        }
    }
}
