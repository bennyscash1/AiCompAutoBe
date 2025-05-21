using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;

namespace ComprehensivePlayrightAuto.MobileTest.MobileTest.RecordAndPlay
{
    [TestFixture, Category(
    Categories.MobileAndroid),
        Category(TestLevel.Level_1)]
    public class MobileRecordAndPlay
    {
        static string runingApp = "chrome";
        public string deviceId = string.Empty;
        [SetUp]
        public async Task SetupMobileRecodr()
        {

            deviceId = await new InitialDeviceServices()
                .PrepareTheDeviceToReadyForRun(runingApp);
            
        }
        [Test]
        public async Task _MobileRecordAndPlay()
        {
            #region Open recording session
            MobileAiDriverFactory mobileRecordDriver = new MobileAiDriverFactory(deviceId, runingApp);
            //await mobileRecordDriver.InitAndroidAppByAppName(deviceId, runingApp);
            MobileBaseFlow mobileRecordFlow = new MobileBaseFlow(mobileRecordDriver.appiumDriver);
            #endregion

            #region Get recording into file
            RecordLocatoreService recordLocatoreService = new RecordLocatoreService();
            string recordFile = recordLocatoreService.CreateRecordFile();

            Process recordProccess = recordLocatoreService.StartAdbRecordingToFile(recordFile);

            Thread.Sleep(1000);
            recordLocatoreService.StopAdbRecording(recordProccess);
            #endregion

            #region Get touch coordinates
            MobileAiDriverFactory mobileRecordDriverx = new MobileAiDriverFactory(deviceId, runingApp);

            MobileBaseFlow mobileRecordFlowx = new MobileBaseFlow(mobileRecordDriverx.appiumDriver);

            await mobileRecordFlowx.ClickOnXyUsingFile(recordFile);
            #endregion
        }
    }
}
