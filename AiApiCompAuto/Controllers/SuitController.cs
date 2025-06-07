using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AiApiCompAuto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuitController : ControllerBase
    {
        [HttpPost("run")]
        public IActionResult RunTest([FromBody] RunSuitCategoryName request)
        {

            #region company branch file detials
            string solutionDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string targetPath = Path.Combine(solutionDir, "MainAiComprehensiveAuto", "AiCompAutoBe");

            #endregion
            #region This run the test suit by category name
            var processInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"test --filter \"Category={request.CategoryName}\"",
                WorkingDirectory = targetPath, // שנה למיקום שלך
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

    }

    public class RunSuitCategoryName
    {
        public string CategoryName { get; set; }
    }
}
