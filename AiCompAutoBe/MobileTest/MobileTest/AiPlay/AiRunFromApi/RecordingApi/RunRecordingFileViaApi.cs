using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
using AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using ComprehensivePlayrightAuto.MobileTest.MobileTest.RecordAndPlay;
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
    public class RunRecordingFileViaApi
    {
        [Test]
        public async Task _RunRecordingFileViaApi(string runingApp, string recordingFileName)
        {
            string deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp,
                EmulatorEnumList.Small_Phone_API_35.ToString());

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, runingApp);
           // await mobileDriver.InitAndroidAppByAppName(deviceId, runingApp);
            MobileAiTaskFlow mobileFlow = new MobileAiTaskFlow(mobileDriver.appiumDriver);

            #region Get recording into file
            string recordFilePath = RecordLocatoreService
                .GetRecordFileFullPath(recordingFileName);
            await mobileFlow.ClickOnXyUsingFile(recordFilePath);
            #endregion
        }
    }
}
