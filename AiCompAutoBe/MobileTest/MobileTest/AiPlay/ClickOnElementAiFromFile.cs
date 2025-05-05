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
using System.Text.Json;
using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;

namespace ComprehensivePlayrightAuto.MobileTest.MobileTest.AiPlay
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class ClickOnElementAiFromFile
    {
        static string runingApp = "Calculator";
        [Test]
        public async Task _ClickOnElementAiFromFile()
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp);

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
            MobileBaseFlow mobileFlow = new MobileBaseFlow(mobileDriver.appiumDriver);

            #region Run the step from file
            var stepsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "step_input.json");
            if (File.Exists(stepsFilePath))
            {
                var stepJson = File.ReadAllText(stepsFilePath);
                var steps = JsonSerializer.Deserialize<List<string>>(stepJson);

                foreach (var step in steps)
                {
                    await mobileFlow.TalkWithApp(step);
                    Console.WriteLine($"Executing step: {step}");
                    // Run custom logic per step
                }
            }
            else
            {
                Console.WriteLine( "THe run file not found or the step is empty");
            }
            #endregion
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
