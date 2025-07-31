using EmployeeLibrary.Helpers;
using EmployeeLibrary.Mdoels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLibrary.Repositories
{
    /// <summary>
    /// Provides CRUD operations for <see cref="Employee"/> entities using <see cref="AppDbContext"/>.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to use for data access.</param>
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>A collection of all <see cref="Employee"/> entities.</returns>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The <see cref="Employee"/> entity if found; otherwise, <c>null</c>.</returns>
        public async Task<Employee?> GetById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> entity to add.</param>
        /// <returns>The unique identifier of the newly created employee.</returns>
        public async Task<int> Create(Employee employee)
        {
            var exists = await _context.Employees.AnyAsync(e => e.Email == employee.Email);
            if (exists)
            {
                throw new Exception(string.Format(Messages.EmployeeAlreadyExist, employee.Email));
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Emp_Id;
        }

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> entity with updated values.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> Update(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.Emp_Id);
            if (existing == null) throw new Exception(string.Format(Messages.EmployeeNotFound, employee.Emp_Id));

            existing.First_Name = employee.First_Name;
            existing.Last_Name = employee.Last_Name;
            existing.Email = employee.Email;
            existing.Phone = employee.Phone;
            existing.Department = employee.Department;
            existing.Hire_Date = employee.Hire_Date;
            existing.Salary = employee.Salary;

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an employee from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>The number of state entries written to the database; 0 if not found.</returns>
        public async Task<int> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) throw new Exception($"Employee ID {id} not found.");
            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync();
        }
    }
}
