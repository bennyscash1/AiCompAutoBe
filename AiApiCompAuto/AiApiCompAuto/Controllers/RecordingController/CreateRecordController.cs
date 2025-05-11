using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateRecordController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] CreateFileInputDto requestData)
        {
            try
            {
                var runingService = new CreateRecordingFileViaApi();
                await runingService._CreateRecordingFileViaApi(
                    requestData.RuningApp,
                    requestData.RecordFileName);
                return Ok(new
                {
                    status = "finished",
                    message = $"The file {requestData.RecordFileName} was create successfully"
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
