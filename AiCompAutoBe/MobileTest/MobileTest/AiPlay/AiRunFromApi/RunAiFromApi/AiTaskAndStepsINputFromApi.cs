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
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;
using AiCompAutoBe.MobileTest.MobileFlows;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class AiTaskAndStepsINputFromApi
    {

        [Test]
        public async Task _AiTaskAndStepsINputFromApi(string runingApp,  
            List<AiTasksList> taskSteps, List<StepInstruction> steps,
            string urlChrome ="", string apiKey ="")
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp,
                EmulatorEnumList.Pixel_2_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
            //await mobileDriver.InitAndroidAppByAppName(deviceId, runingApp);
            //MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);
            if (runingApp =="chrome")
            {
                var mobileTaskFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);
                if (string.IsNullOrEmpty(urlChrome))
                {
                    mobileTaskFlow.InitChromeToSearch(false);
                }
                else
                {
                    mobileTaskFlow.InitChromeToSearch(true, urlChrome);
                }
            }
            #region Get the ai task 
            if (taskSteps != null && taskSteps.Any(ts => !string.IsNullOrWhiteSpace(ts.TaskStep)))
            {
                foreach (var taskStep in taskSteps)
                {
                    var mobileTaskFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver); 
                    await mobileTaskFlow.HandleAiTaskMission(taskStep.TaskStep, apiKey);
                    Console.WriteLine($"Executing task step: {taskStep}");
                }
            }
            #endregion
            #region Run the step from api request
            if (steps != null && steps.Any())
            {
                foreach (var step in steps)
                {
                    var mobileSingleFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver); 
                    await mobileSingleFlow.TalkWithApp(step.ElementView, step.InputText, apiKey);
                    Console.WriteLine($"Executing click step: {step}");
                }
            }

            else
            {
                Console.WriteLine("No steps provided.");
            }
            #endregion
        }
    }
}
