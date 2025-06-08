/*using AiApiCompAuto.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi;
using AiApiCompAuto.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;

using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //Not used
    public class CreateRecordController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] CreateFileInputDto requestData)
        {
            try
            {
                //It not been used!!!!!
                var runingService = new CreateRecordingFileViaApi();
                await runingService.StartRecordingAsync(
                    requestData.RuningApp,
                    requestData.RecordFileName);
                Console.WriteLine($"The creat file create {requestData.RecordFileName}");
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
*/