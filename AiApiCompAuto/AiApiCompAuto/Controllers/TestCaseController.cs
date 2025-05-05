using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComprehensivePlayrightAuto.MobileTest.MobileTest.AiPlay;

namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCaseController : ControllerBase
    {
        [HttpPost("run")]
        public async Task<IActionResult> RunTestCase()
        {
            try
            {
                var aiService = new MobileClickUsingAI(); 
                await aiService._MobileClickUsingAI();

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
