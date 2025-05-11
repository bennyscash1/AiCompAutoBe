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
using AiCompAutoBe.MobileTest.MobileFlows;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class AiTaskAndInputElemnts
    {

        [Test]
        public async Task _AiTaskAndInputElemnts(string runingApp, List<AiTasksList>taskSteps, List<StepInstruction> steps )
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp, 
                EmulatorEnumList.Small_Phone_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            #region Get the ai task 
            if (taskSteps != null && taskSteps.Any())
            {
                foreach (var taskStep in taskSteps)
                {
                    await mobileFlow.HandleAiTaskMission(taskStep.TaskStep);
                    Console.WriteLine($"Executing task step: {taskStep}");
                }
            }
            #endregion
            #region Run the step from api request
            if (steps != null && steps.Any())
            {
                foreach (var step in steps)
                {
                    await mobileFlow.TalkWithApp(step.ElementView, step.InputText);
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
