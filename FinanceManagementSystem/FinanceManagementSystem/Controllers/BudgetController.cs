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
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly ILogger<BudgetController> _logger;
        /// <summary>
        /// Controller will Initilize the BudgetService and Logger Interfaces
        /// </summary>
        /// <param name="budgetService">Object of IBudgetService</param>
        /// <param name="logger">Object of ILogger</param>
        public BudgetController(IBudgetService budgetService, ILogger<BudgetController> logger)
        {
            _budgetService = budgetService;
            _logger = logger;
        }
        /// <summary>
        /// Get all the records of Budgets Table
        /// </summary>
        /// <returns>List of records in Budgets Table</returns>
        [HttpGet]
       public async Task<IEnumerable<Budget>> GetBudgetAsync()
        {
            _logger.LogInformation("Received a Get Request");
            return await _budgetService.GetBudgetAsync();
        }
        /// <summary>
        /// Get the perticular record based on Id
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Record corresponding to given Id</returns>
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetAsyncById(int id)
        {
            _logger.LogInformation("Received a Get Request By Id");
            var income = await _budgetService.GetBudgetAsyncById(id);
            if (income == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(income);
        }
        /// <summary>
        /// Add the records into  Budget Table
        /// </summary>
        /// <param name="budgetDto">Budget DTO object</param>
        /// <returns>Newly created Record</returns>
        [HttpPost]
        public async Task<IActionResult> PostBudgetAsync(BudgetDto budgetDto)
        {
            _logger.LogInformation("Received a Add Request");

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided." });

            var createdIncome = await _budgetService.PostBudgetAsync(budgetDto);
            return CreatedAtAction(nameof(GetBudgetAsyncById), new { id = createdIncome.BudgetId }, createdIncome);
        }
        /// <summary>
        /// Update the previous records
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <param name="budgetDto">Budget DTO object</param>
        /// <returns>Newly updated record</returns>

        [HttpPut]
        public async Task<IActionResult> UpdateIncomeAsync(int id, BudgetDto budgetDto)
        {
            _logger.LogInformation("Received a update Request");
            if (id != budgetDto.BudgetId)
                return BadRequest(new { Message = "ID mismatch between the route and payload." });

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided."});

            var isUpdated = await _budgetService.UpdateBudgetAsync(id, budgetDto);
            if (isUpdated == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(isUpdated);
        }
        /// <summary>
        /// Delete the records based on Id
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Remaining records after deletion</returns>
        [HttpDelete]

        public async Task<IActionResult> DeleteBudgetAsync(int id)
        {
            _logger.LogInformation("Received a Delete Request");
            var isDeleted = await _budgetService.DeleteBudgetAsync(id);
            if (!isDeleted)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return NoContent();
        }


    }
}
