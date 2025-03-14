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
            _logger.LogInformation("Este es un log de nivel Information desde /info");
            return Ok("Endpoint de prueba: nivel Information");
        }

        // GET: api/test/warning
        // Logs a warning message and returns a simple string response
        [HttpGet("warning")]
        public IActionResult GetWarning()
        {
            _logger.LogWarning("Este es un log de nivel Warning desde /warning");
            return Ok("Endpoint de prueba: nivel Warning");
        }

        // GET: api/test/error
        // Logs an error message, simulates an exception, catches it, and logs the exception details
        [HttpGet("error")]
        public IActionResult GetError()
        {
            _logger.LogError("Este es un log de nivel Error desde /error");
            try
            {
                // Simulate an exception to test error logging
                throw new Exception("Excepción simulada para probar error");
            }
            catch (Exception ex)
            {
                // Log the caught exception with error level
                _logger.LogError(ex, "Excepción capturada en /error");
            }
            return Ok("Endpoint de prueba: nivel Error (revisa los logs)");
        }
    }
}
