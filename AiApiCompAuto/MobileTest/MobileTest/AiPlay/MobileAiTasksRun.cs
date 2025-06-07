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

namespace AiApiCompAuto.MobileTest.MobileTest.AiPlay
{
    [TestFixture, Category(
        Categories.MobileAndroid),
    Category(TestLevel.Level_1)]
    public class MobileAiTasksRun
    {
        static string appRuningName = "Chrome";
        public string deviceId = string.Empty;

        [Test]
        public async Task _MobileAiTasksRun()
        {
            deviceId = await new InitialDeviceServices()
           .PrepareTheDeviceToReadyForRun(appRuningName);

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory();
            await mobileDriver.InitAndroidAppByAppName(deviceId, appRuningName);
            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            //Click on app buttons
            await mobileFlow.HandleAiTaskMission(
                   "YOur mission is to navigate to the search google page, approve and confirm all buttons till you get to this page.");
            await mobileFlow.TalkWithApp("Click on number 9");
            

        }

    }
}
