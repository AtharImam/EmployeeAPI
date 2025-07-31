using EmployeeLibrary.Mdoels;

namespace EmployeeLibrary.Repositories
{
    /// <summary>
    /// Defines methods for managing employee data in the repository.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all employees from the repository.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of <see cref="Employee"/> objects.
        /// </returns>
        Task<IEnumerable<Employee>> GetAll();

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the <see cref="Employee"/> if found; otherwise, <c>null</c>.
        /// </returns>
        Task<Employee?> GetById(int id);

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> entity to add.</param>
        /// <returns>The unique identifier of the newly created employee.</returns>
        Task<int> Create(Employee employee);

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> entity with updated values.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> Update(Employee employee);

        /// <summary>
        /// Deletes an employee from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>The number of state entries written to the database; 0 if not found.</returns>
        Task<int> Delete(int id);
    }
}
