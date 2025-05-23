﻿using AiCompAutoBe.MobileTest.MobileTest;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using AiCompAutoBe.MobileTest.MobileTest.GetDeviceAppData;
using Microsoft.AspNetCore.Mvc;


namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAppAndDeviceNameController : ControllerBase
    {
        [HttpGet("run")]
        public async Task<IActionResult> RunApiSteps()
        {
            try
            {
                var runingService = new GetAppForgroundName();
                string appName = await runingService._GetAppForgroundName();
                return Ok(new
                {
                    status = "finished",
                    message = $"{appName}"
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
