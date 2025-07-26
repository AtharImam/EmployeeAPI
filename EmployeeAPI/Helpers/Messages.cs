namespace EmployeeAPI.Helpers
{
    public static class Messages
    {
        public const string EmployeeNotFound = "Employee with ID {0} not found.";
        public const string EmployeeCreated = "Employee created successfully.";
        public const string EmployeeCreationFailed = "Failed to create employee.";
        public const string EmployeeUpdateNotFound = "Cannot update. Employee with ID {0} does not exist.";
        public const string EmployeeUpdated = "Employee with ID {0} updated successfully.";
        public const string EmployeeDeleteNotFound = "Cannot delete. Employee with ID {0} does not exist.";
        public const string EmployeeDeleted = "Employee with ID {0} deleted successfully.";
        public const string EmployeeAlreadyExist = "An employee with email '{0}' already exists.";
    }
}
