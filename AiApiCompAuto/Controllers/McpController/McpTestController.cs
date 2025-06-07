using AiApiCompAuto.Infra.McpService;
using AiApiCompAuto.MobileTest.MobileTest.AiPlay;
using Microsoft.AspNetCore.Mvc;

namespace AiApiCompAuto.Controllers.McpController
{
    [ApiController]
    [Route("api/mcp/[controller]")]
    public class McpMobileController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> Run([FromBody] McpRunRequest request)
        {
            var runner = new MobileMcpLocalRun();
            string result = await runner.RunMcpTask(request.Input);

            return Ok(new
            {
                goal = request.Input,
                result
            });
        }

        [HttpPost("run-smoke-test")]
        public IActionResult RunSmokeTest()
        {
            var result = McpService.RunMobileSmokeTest();
            return Ok(result);
        }

        public class McpRunRequest
        {
            public string Input { get; set; }
        }

    }
}
