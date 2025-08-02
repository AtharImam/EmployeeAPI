
using EmployeeAPI.Data;

namespace EmployeeAPI.Api
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
