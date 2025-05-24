using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AiCompAutoBe.MobileTest.MobileFlows.MobileAiTaskFlow;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay
{
    [TestFixture, Category(
        Categories.MobileAndroid),
    Category(TestLevel.Level_1)]
    public class MobileAiTasksInitChromeRun
    {
        static string appRuningName = "chrome";
        public string deviceId = string.Empty;

        [Test]
        public async Task _MobileAiTasksInitChromeRun()
        {
            deviceId = await new InitialDeviceServices()
           .PrepareTheDeviceToReadyForRun(appRuningName);

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, appRuningName);
            await mobileDriver.InitAndroidAppByAppName(deviceId, appRuningName);
            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            //Click on app buttons
            mobileFlow.InitChromeToSearch(true, "https://recaptcha-demo.appspot.com/recaptcha-v2-checkbox.php");
            int aiResponceType = await mobileFlow.HandleAiTaskMission(
                   "Click on the checbox 'i'm not a robot'");      
            await mobileFlow.TalkWithApp("Click on number 9");
            

        }

    }
}
