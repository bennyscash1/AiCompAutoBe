using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;

using AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using Microsoft.AspNetCore.Mvc;
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;
using System.Diagnostics;
using AiApiCompAuto.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StartRecordingController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunApiSteps([FromBody] CreateFileInputDto requestData)
        {
            try
            {
                string deviceId = await new InitialDeviceServices()
                  .PrepareTheDeviceToReadyForRun(requestData.RuningApp,
                  EmulatorEnumList.Small_Phone_API_35.ToString());
                Console.WriteLine("Device create id: " + deviceId);
                MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, requestData.RuningApp);
                MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

                var recordService = new RecordLocatoreService();
                string file = recordService.CreateRecordFile(requestData.RecordFileName);
                Console.WriteLine("File record was create :" + file);
                Process recordingProcess = recordService
                    .StartAdbRecordingToFile(file);

                RecordingSessionStore.CurrentRecordingProcess = recordingProcess;
                RecordingSessionStore.CurrentRecordingFile = file;


                return Ok(new
                {
                    status = "finished",
                    message = $"The recording start correct on path :{file}\n" +
                    $"You can record your app now"
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
