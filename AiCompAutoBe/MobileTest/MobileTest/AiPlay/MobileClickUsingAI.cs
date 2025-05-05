using ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComprehensiveAutomation.MobileTest.Inital;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V117.Runtime;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;

namespace ComprehensivePlayrightAuto.MobileTest.MobileTest.AiPlay
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class MobileClickUsingAI
    {
        static string appRuningName = "Calculator";
        public  string deviceId = string.Empty;
        [SetUp]
        public async Task SetupMobileDevice()
        {
            deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(appRuningName);
        }
        [Test]
        public async Task _MobileClickUsingAI()
        {
            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, appRuningName);
            MobileBaseFlow mobileFlow = new MobileBaseFlow(mobileDriver.appiumDriver);

            //Click on app buttons
            await mobileFlow.TalkWithApp("תלחץ על כפתור נקודה");
            await mobileFlow.TalkWithApp("Click on AC button");
            await mobileFlow.TalkWithApp("Click on number 8");
            await mobileFlow.TalkWithApp("Click on =");
        }





        //await mobileLoginFlow.TalkWithYouApp("Click on number 5");
        //await mobileLoginFlow.TalkWithYouApp("Plus button");
        //await mobileLoginFlow.TalkWithYouApp("Click on number 8");
        //await mobileLoginFlow.TalkWithYouApp("Click on =");

        /*        await mobileLoginFlow.TalkWithYouApp("Click on 'use wihtout an account'");
                await mobileLoginFlow.TalkWithYouApp("More button");
                await mobileLoginFlow.TalkWithYouApp("Click on 'Got it'");
                await mobileLoginFlow.TalkWithYouApp("Search input field", "Automatico");*/
    }
}
