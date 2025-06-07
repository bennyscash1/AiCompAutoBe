
using AiApiCompAuto.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StopRecordingController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] CreateFileInputDto requestData)
        {
            try
            {
                var recordService = new RecordLocatoreService();

                var process = RecordingSessionStore.CurrentRecordingProcess;
                if (process == null)
                    return BadRequest("No active recording session found.");
                await Task.Delay(500);

                recordService.StopAdbRecording(process);

                string file = RecordingSessionStore.CurrentRecordingFile;
                RecordingSessionStore.CurrentRecordingProcess = null;
                RecordingSessionStore.CurrentRecordingFile = null;

                return Ok(new
                {
                    status = "finished",
                    message = $"The recording file was saved on path: '{file}'"
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
