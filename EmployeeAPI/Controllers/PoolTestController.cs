//using EmployeeAPI.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace EmployeeAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PoolTestController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public PoolTestController(AppDbContext context)
//        {
//            _context = context;
//        }

//        /// <summary>
//        /// Simulates opening a database connection, holds it open for 3 seconds,
//        /// and then closes it. Useful for testing connection pool behavior under load.
//        /// </summary>
//        [HttpGet("simulate")]
//        public async Task<IActionResult> SimulateOpenConnection()
//        {
//            var conn = _context.Database.GetDbConnection();

//            try
//            {
//                await conn.OpenAsync();

//                Console.WriteLine($"🔌 Connection opened. Thread ID: {Environment.CurrentManagedThreadId}");

//                await Task.Delay(3000);

//                return Ok("Connection was held open for 3 seconds.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Error: {ex.Message}");
//            }
//            finally
//            {
//                if (conn.State == System.Data.ConnectionState.Open)
//                {
//                    await conn.CloseAsync();
//                    Console.WriteLine("Connection closed.");
//                }
//            }
//        }
//    }
//}
