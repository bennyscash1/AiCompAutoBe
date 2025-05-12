using CliWrap;
using CliWrap.Buffered;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;

namespace AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService
{
    public class InitialDeviceServices
    {
        public async Task <string>PrepareTheDeviceToReadyForRun(string runingApp, 
            string deviceName ="")
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                deviceName = EmulatorEnumList.Pixel_2_API_35.ToString();
            }
            //Run deviece
            string deviceId = new MobileEmulatorMenegar ()
                .EnsureDeviceIsRunning(deviceName);
            //Run appium
            await new AppiumMenegar().RunAppiumServer();

            return deviceId;
        }
        public async Task<string> RunCliCommand(string command, string type = "adb")
        {
            var result = await Cli.Wrap(type)
                .WithArguments(command)
                .ExecuteBufferedAsync();

            return result.StandardOutput;
        }

    }
}
