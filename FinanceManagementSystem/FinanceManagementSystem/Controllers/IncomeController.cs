using FinanceManagementSystem.DTO;
using FinanceManagementSystem.IServices;
using FinanceManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly ILogger<IncomeController> _logger;
        public IncomeController(IIncomeService incomeService, ILogger<IncomeController> logger)
        {
            _incomeService = incomeService;
            _logger = logger;
        }

        /// <summary>
        /// Get all the records from Income table
        /// </summary>
        /// <returns>List of records of Income table</returns>

        [HttpGet]
        public async Task<IActionResult> GetIncomeAsync()
        {
            _logger.LogInformation("Received a Get Request");
            var income = await _incomeService.GetIncomeAsync();
            return Ok(income);
        }

        /// <summary>
        /// Get the records based on Id
        /// </summary>
        /// <param name="id">Income Id</param>
        /// <returns>Perticular records corresponding to the Id given.</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeAsyncById(int id)
        {
            _logger.LogInformation("Received a Get Request By Id");
            var income = await _incomeService.GetIncomeAsyncById(id);
            if (income == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(income);
        }

        /// <summary>
        /// Add the new records to the Income table
        /// </summary>
        /// <param name="incomeDto">Income DTO object</param>
        /// <returns>Newly added record into database</returns>

        [HttpPost]
        public async Task<IActionResult> PostIncomeAsync(IncomeDto incomeDto)
        {
            _logger.LogInformation("Received a Add Request");
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var createdIncome = await _incomeService.PostIncomeAsync(incomeDto);
            return CreatedAtAction(nameof(GetIncomeAsyncById), new { id = createdIncome.IncomeId }, createdIncome);
        }

        /// <summary>
        /// Update the old records using Id
        /// </summary>
        /// <param name="id">Income Id</param>
        /// <param name="incomeDto">Income Dto object</param>
        /// <returns></returns>

        [HttpPut]
        public async Task<IActionResult> UpdateIncomeAsync(int id, IncomeDto incomeDto)
        {
            _logger.LogInformation("Received a put Request ");
            if (id != incomeDto.IncomeId)
                return BadRequest(new { Message = "ID mismatch between the route and payload." });

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var updatedIncome = await _incomeService.UpdateIncomeAsync(id, incomeDto);
            if (updatedIncome == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(updatedIncome); // Return the updated resource.
        }

        /// <summary>
        /// Delete the record based on Id
        /// </summary>
        /// <param name="id">Income ID</param>
        /// <returns>Remaining records after deletion</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeAsync(int id)
        {
            _logger.LogInformation("Received a Delete Request");
            var isDeleted = await _incomeService.DeleteIncomeAsync(id);
            if (!isDeleted)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return NoContent();
        }
    }
}
