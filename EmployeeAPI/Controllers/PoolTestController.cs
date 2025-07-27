using EmployeeAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/pool")]
    public class PoolTestController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<PoolTestController> _logger;

        public PoolTestController(AppDbContext db, ILogger<PoolTestController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestConnection()
        {
            var pod = Environment.GetEnvironmentVariable("POD_Name") ?? "unknown";
            var conn = _db.Database.GetDbConnection();

            try
            {
                await conn.OpenAsync();

                _logger.LogInformation($"[{pod}] Opened DB connection at {DateTime.UtcNow}");

                await Task.Delay(3000);

                await conn.CloseAsync();
                _logger.LogInformation($"[{pod}] Closed DB connection at {DateTime.UtcNow}");

                return Ok($"[{pod}] Connection test completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{pod}] Connection test failed");
                return StatusCode(500, "DB connection failed");
            }
        }
    }
}
