using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay;
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
             Category(TestLevel.Level_2)]
    public class CreateRecordFile
    {
        public static string runingApp = "calculator";
        public string deviceId = string.Empty;
        public static string recordFileName = "zzzzzzz";

        [SetUp]
        public async Task _SetupMobile()
        {
            deviceId = await new InitialDeviceServices()
                 .PrepareTheDeviceToReadyForRun(runingApp);
        }
        [Test]
        public async Task _CreateRecordFile()
        {
            #region Open recording session
            MobileAiDriverFactory mobileRecordDriver = new MobileAiDriverFactory();
            await mobileRecordDriver.InitAndroidAppByAppName(deviceId, runingApp);
            MobileBaseFlow mobileRecordFlow = new MobileBaseFlow(mobileRecordDriver.appiumDriver);
            #endregion

            var recordLocatoreService = new RecordLocatoreService();
            string recordFile = recordLocatoreService.CreateRecordFile(recordFileName);

            Process recordProcess = recordLocatoreService.StartAdbRecordingToFile(recordFile);

            // ✅ Store for later use
            RecordingSessionStore.CurrentRecordingProcess = recordProcess;

            //here do somthing that i 
            // Store the process globally
            // RecordingSessionStore.CurrentRecordingProcess = recordProccess;

            /*          StopRecordRun stopRecordRun = new StopRecordRun();
                      await stopRecordRun._StopRecordRun();*/
        }

    }
}
