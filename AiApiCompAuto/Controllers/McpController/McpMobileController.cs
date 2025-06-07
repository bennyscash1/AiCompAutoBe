using AiApiCompAuto.Infra.McpService;
using Microsoft.AspNetCore.Mvc;

namespace AiApiCompAuto.Controllers.McpController
{
    [ApiController]
    [Route("api/mcp/[controller]")]
    public class McpTestController : ControllerBase
    {
        [HttpGet("echo")]
        public IActionResult Echo([FromQuery] string input)
        {
            var result = McpService.Echo(input);
            return Ok(new
            {
                status = "finished",
                message = $"Mcp return {result}"
            });
        }

        [HttpPost("run-smoke-test")]
        public IActionResult RunSmokeTest()
        {
            var result = McpService.RunMobileSmokeTest();
            return Ok(result);
        }
    }
}
