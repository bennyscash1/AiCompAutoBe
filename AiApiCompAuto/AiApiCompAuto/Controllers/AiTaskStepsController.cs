using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
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
                var runingService = new AiTaskAndStepsINputFromApi();
                await runingService._AiTaskAndStepsINputFromApi(
                    requestData.RuningApp,           
                    requestData.AiTasksList,
                    requestData.TestInputSteps,
                    requestData.UrlForChrome, 
                    requestData.AnthropicKey);
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
