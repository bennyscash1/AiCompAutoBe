using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi;
using ComprehensivePlayrightAuto.MobileTest.MobileTest.AiPlay;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiClickStepsController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] RunTestCaseName request)
        {
            try
            {
                var runingService = new ClickElementsFromApi();
                await runingService._ClickElementsFromApi(request.runingApp, request.StepTest);
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
            public string runingApp { get; set; } // optional
            public List<string>? StepTest { get; set; } // optional
        }
    }
}
