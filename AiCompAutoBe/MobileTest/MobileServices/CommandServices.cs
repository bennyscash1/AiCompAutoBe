using CliWrap.Buffered;
using CliWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AiCompAutoBe.MobileTest.MobileServices
{
    public class CommandServices
    {

        public async Task<string> RunAdbShell(string command)
        {
            var cmd = Cli.Wrap("adb")
                .WithArguments($"shell {command}"); // run `adb shell <your command>`
            BufferedCommandResult result = await cmd.ExecuteBufferedAsync();
            return result.StandardOutput.Trim();
        }

        public static string RunAdbCommand(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "adb",
                    Arguments = command, // use the parameter value, not the string "command"
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            return string.IsNullOrWhiteSpace(output) ? error : output;
        }
    }
}
