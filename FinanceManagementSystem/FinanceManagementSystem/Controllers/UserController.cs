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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get all the records from the User Table
        /// </summary>
        /// <returns>List of records in the User Table</returns>

        [HttpGet]
        public async Task<IEnumerable<User>> GetUser()
        {
            _logger.LogInformation("Received a Get Request");
            return (IEnumerable<User>)await _userService.GetUser();
        }
        /// <summary>
        /// Get record based on the Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Perticular Record based corresponding to Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation("Received a Get Request By Id");
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(user);
        }

        /// <summary>
        /// Add the new records to  User Table
        /// </summary>
        /// <param name="userDto">UserDto</param>
        /// <returns>Newly added record to the database</returns>

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            _logger.LogInformation("Received a Add Request");
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var createdIncome = await _userService.AddUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdIncome.UserId }, createdIncome);
        }

        /// <summary>
        /// Update the record based on Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="userDto">User DTO Object</param>
        /// <returns>updated record to the perticular Id</returns>

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            _logger.LogInformation("Received a put Request");
            if (id != userDto.UserId)
                return BadRequest(new { Message = "ID mismatch between the route and payload." });

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var isUpdated = await _userService.UpdateUser(id, userDto);
            if (isUpdated == null)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return Ok(isUpdated);
        }

        /// <summary>
        /// Delete the records based on Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Remaining records after the deletion</returns>
        
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Received a Delete Request");
            var isDeleted = await _userService.DeleteUser(id);
            if (!isDeleted)
                return NotFound(new { Message = $"Income with ID {id} not found." });

            return NoContent();
        }
    }
}
