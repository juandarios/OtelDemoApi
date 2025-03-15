using Microsoft.AspNetCore.Mvc;

namespace OtelDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        // GET: api/test/info
        // Logs an informational message and returns a simple string response
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            _logger.LogInformation("This is an Information level log from /info");
            return Ok("Test endpoint: Information level");
        }

        // GET: api/test/warning
        // Logs a warning message and returns a simple string response
        [HttpGet("warning")]
        public IActionResult GetWarning()
        {
            _logger.LogWarning("This is a Warning level log from /warning");
            return Ok("Test endpoint: Warning level");
        }

        // GET: api/test/error
        // Logs an error message, simulates an exception, catches it, and logs the exception details
        [HttpGet("error")]
        public IActionResult GetError()
        {
            _logger.LogError("This is an Error level log from /error");
            try
            {
                // Simulate an exception to test error logging
                throw new Exception("Simulated exception to test error");
            }
            catch (Exception ex)
            {
                // Log the caught exception with error level
                _logger.LogError(ex, "Exception caught in /error");
            }
            return Ok("Test endpoint: Error level (check the logs)");
        }
    }
}