using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiTaskStepsController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] TestInputData requestData)
        {
            try
            {
                var runingService = new AiTaskAndInputElemnts();
                await runingService._AiTaskAndInputElemnts(
                    requestData.RuningApp,
                    requestData.AiTaskRequest,
                    requestData.TestInputSteps);
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
    }
}
