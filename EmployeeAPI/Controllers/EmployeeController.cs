using EmployeeLibrary.Helpers;
using EmployeeLibrary.Mdoels;
using EmployeeLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    /// <summary>
    /// API controller for managing <see cref="Employee"/> entities.
    /// Provides endpoints for CRUD operations such as retrieving, creating, updating, and deleting employees.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="repo">The repository used for employee data access.</param>
        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Test endpoint to simulate load and verify connection pooling.
        /// </summary>
        /// <returns>A simple response after a delay.</returns>
        [HttpGet("test")]
        public async Task<IActionResult> TestConnectionPooling()
        {
            var podName = Environment.GetEnvironmentVariable("POD_Name") ?? "unknown-pod";
            Console.WriteLine($"[LOG] Connection test hit from POD: {podName}");

            var employee = await _repo.GetFirstOrDefault();

            await Task.Delay(1500);

            return Ok(new
            {
                Message = "Connection pooling test complete.",
                Pod = podName,
                Employee = employee
            });
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of all <see cref="Employee"/> entities.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _repo.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The <see cref="Employee"/> entity if found; otherwise, a NotFound result.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _repo.GetById(id);
            if (employee == null)
            {
                return NotFound(new { message = string.Format(Messages.EmployeeNotFound, id) });
            }
            return Ok(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> entity to create.</param>
        /// <returns>The unique identifier of the newly created employee if successful; otherwise, a BadRequest result.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                int newId = await _repo.Create(employee);
                if (newId > 0)
                {
                    return Ok(new { message = Messages.EmployeeCreated, employeeId = newId });
                }
                return BadRequest(new { message = Messages.EmployeeCreationFailed });
            }
            catch (Exception ex) when (ex.Message.Contains("already exists"))
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to update.</param>
        /// <param name="employee">The <see cref="Employee"/> entity with updated values.</param>
        /// <returns>An Ok result if successful; otherwise, a NotFound result.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
            {
                return NotFound(new { message = string.Format(Messages.EmployeeUpdateNotFound, id) });
            }

            employee.Emp_Id = id;
            await _repo.Update(employee);
            return Ok(new { message = string.Format(Messages.EmployeeUpdated, id) });
        }

        /// <summary>
        /// Deletes an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>An Ok result if successful; otherwise, a NotFound result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
            {
                return NotFound(new { message = string.Format(Messages.EmployeeDeleteNotFound, id) });
            }

            await _repo.Delete(id);
            return Ok(new { message = string.Format(Messages.EmployeeDeleted, id) });
        }
    }
}
