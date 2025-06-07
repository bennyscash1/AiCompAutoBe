using AiApiCompAuto.MobileTest.MobileTest.AiPlay;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileStepsController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunFileSteps([FromBody] RunTestCaseName request)
        {

            try
            {
                var runingService = new ClickOnElementAiFromFile();
                await runingService._ClickOnElementAiFromFile();
                return Ok(new
                {
                    status = "finished",
                    message = "Test executed successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message
                });
            }      
        }
        public class RunTestCaseName
        {
            public string TestCaseName { get; set; }
            public List<string>? StepTest { get; set; } // optional
        }
    }
}
