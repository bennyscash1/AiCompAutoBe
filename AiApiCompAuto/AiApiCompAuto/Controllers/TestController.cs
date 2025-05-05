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
            #region Get the solution directory file
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string targetPath = Path.Combine(solutionDir, "MainAiComprehensiveAuto", "AiCompAutoBe");
            #endregion

            #region Get path of the step test if it exists
            if (request.StepTest != null && request.StepTest.Any())
            {
                string stepFilePath = Path.Combine(targetPath, "step_input.json");
                System.IO.File.WriteAllText(stepFilePath, System.Text.Json.JsonSerializer.Serialize(request.StepTest));
            }
            #endregion
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
            process.WaitForExit(); 

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

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
            public List<string>? StepTest { get; set; } // optional
        }
    }
}
