using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService
{
    public class PreparingDeviceToRun
    {
        public async Task <string>PrepareTheDeviceToReadyForRun(string runingApp, 
            string deviceName ="")
        {            
            deviceName ??= MobileAiDriverFactory.MobileDeviceName;
            //Run deviece
            string deviceId = new MobileEmulatorMenegar ().EnsureDeviceIsRunning();
            //Run appium
            await new AppiumMenegar().RunAppiumServer();

            return deviceId;
        }
    
    }
}
