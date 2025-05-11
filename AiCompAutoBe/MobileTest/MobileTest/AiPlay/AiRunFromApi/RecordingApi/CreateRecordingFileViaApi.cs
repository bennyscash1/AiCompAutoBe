using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class CreateRecordingFileViaApi
    {
        [Test]
        public async Task _CreateRecordingFileViaApi(string runingApp, string recordingFileName)
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp,
                EmulatorEnumList.Small_Phone_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            #region Get recording into file
            RecordLocatoreService recordLocatoreService = new RecordLocatoreService();
            string recordFile = recordLocatoreService.CreateRecordFile(recordingFileName);

            Process recordProccess = recordLocatoreService.StartAdbRecordingToFile(recordFile);
            Thread.Sleep(1000);
            recordLocatoreService.StopAdbRecording(recordProccess);
            #endregion
        }
    }
}
