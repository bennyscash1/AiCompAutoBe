using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RunRecordFileController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] CreateFileInputDto requestData)
        {
            try
            {
                var runingService = new RunRecordingFileViaApi();
                await runingService._RunRecordingFileViaApi(
                    requestData.RuningApp,
                    requestData.RecordFileName);
                return Ok(new
                {
                    status = "finished",
                    message = $"The file {requestData.RecordFileName} was run successfully"
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
