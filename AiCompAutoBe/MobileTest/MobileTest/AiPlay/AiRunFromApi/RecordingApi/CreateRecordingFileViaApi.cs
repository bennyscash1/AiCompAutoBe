using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay;
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
        public async Task<string> StartRecordingAsync(string runingApp, string recordingFileName)
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp, EmulatorEnumList.Small_Phone_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory();
            await mobileDriver.InitAndroidAppByAppName(deviceId, runingApp);

            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            RecordLocatoreService recordLocatoreService = new RecordLocatoreService();
            string recordFile = recordLocatoreService.CreateRecordFile(recordingFileName);

            Process recordProcess = recordLocatoreService.StartAdbRecordingToFile(recordFile);

            // ✅ Store recording session globally
            RecordingSessionStore.CurrentRecordingProcess = recordProcess;
            RecordingSessionStore.CurrentRecordingFile = recordFile;

            return recordFile;
        }

        public void StopRecording()
        {
            var process = RecordingSessionStore.CurrentRecordingProcess;
            if (process != null)
            {
                new RecordLocatoreService().StopAdbRecording(process);
            }

            // Optional cleanup
            RecordingSessionStore.CurrentRecordingProcess = null;
        }
    }

}
