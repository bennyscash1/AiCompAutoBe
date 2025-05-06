using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiInputsStepsController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] TestInputData requestData)
        {
            try
            {
                var runingService = new InputElementsFromApi();
                await runingService._InputElementsFromApi(
                    requestData.RuningApp,
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
