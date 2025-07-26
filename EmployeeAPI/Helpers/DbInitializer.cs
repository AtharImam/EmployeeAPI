using EmployeeAPI.Data;
using EmployeeAPI.Models;

namespace EmployeeAPI.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated(); // or use context.Database.Migrate();

            if (context.Employees.Any())
                return; // DB has already been seeded

            var employees = new List<Employee>
            {
                new Employee { First_Name = "John", Last_Name = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Department = "Engineering", Hire_Date = DateTime.Parse("2021-01-10"), Salary = 60000.00m },
                new Employee { First_Name = "Jane", Last_Name = "Smith", Email = "jane.smith@example.com", Phone = "9876543210", Department = "HR", Hire_Date = DateTime.Parse("2022-03-15"), Salary = 55000.00m },
                new Employee { First_Name = "Bob", Last_Name = "Williams", Email = "bob.williams@example.com", Phone = "5555555555", Department = "Marketing", Hire_Date = DateTime.Parse("2020-07-25"), Salary = 50000.00m },
                new Employee { First_Name = "Alice", Last_Name = "Johnson", Email = "alice.johnson@example.com", Phone = "4444444444", Department = "Sales", Hire_Date = DateTime.Parse("2019-12-01"), Salary = 62000.00m },
                new Employee { First_Name = "Tom", Last_Name = "Brown", Email = "tom.brown@example.com", Phone = "3333333333", Department = "Support", Hire_Date = DateTime.Parse("2023-06-30"), Salary = 48000.00m },
                new Employee { First_Name = "Sara", Last_Name = "Davis", Email = "sara.davis@example.com", Phone = "2222222222", Department = "Finance", Hire_Date = DateTime.Parse("2021-11-20"), Salary = 70000.00m },
                new Employee { First_Name = "Mike", Last_Name = "Miller", Email = "mike.miller@example.com", Phone = "1111111111", Department = "Legal", Hire_Date = DateTime.Parse("2022-09-10"), Salary = 67000.00m }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
