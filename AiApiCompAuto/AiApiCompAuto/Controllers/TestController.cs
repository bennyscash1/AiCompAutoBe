using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost("run")]
        public IActionResult RunTestCase([FromBody] RunTestCaseName request)
        {
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string targetPath = Path.Combine(solutionDir, "AiComprehensiveAuto", "AiCompAutoBe");

            #region This run the test case by name
            var processInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"test --filter \"FullyQualifiedName~{request.TestCaseName}\"",
                WorkingDirectory = targetPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            #endregion
            var process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return Ok(new
            {
                status = "finished",
                exitCode = process.ExitCode,
                output,
                error
            });
        }
        public class RunTestCaseName
        {
            public string TestCaseName { get; set; }
        }
    }
}
