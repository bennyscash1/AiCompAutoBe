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
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi;
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class InputElementsFromApi
    {

        [Test]
        public async Task _InputElementsFromApi(string runingApp, List<StepInstruction> steps)
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp, 
                EmulatorEnumList.Small_Phone_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
            MobileBaseFlow mobileFlow = new MobileBaseFlow(mobileDriver.appiumDriver);

            #region Run the step from api request
            if (steps != null && steps.Any())
            {
                foreach (var step in steps)
                {
                    await mobileFlow.TalkWithApp(step.ElementView, step.InputText);
                    Console.WriteLine($"Executing step: {step}");
                }
            }
            else
            {
                Console.WriteLine("No steps provided.");
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
