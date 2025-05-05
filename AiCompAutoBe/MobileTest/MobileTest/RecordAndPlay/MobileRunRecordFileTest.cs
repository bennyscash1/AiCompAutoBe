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
    public class MobileRunRecordFileTest
    {
        static string runingApp = CreateRecordFile.runingApp;
        public string deviceId = string.Empty;
        [SetUp]
        public async Task SetupMobile()
        {
            deviceId = await new PreparingDeviceToRun()
               .PrepareTheDeviceToReadyForRun(runingApp);
        }
        [Test]
        public async Task _MobileRunRecordFileTest()
        {
            #region Open recording session
            MobileAiDriverFactory mobileRecordDriver = new MobileAiDriverFactory(deviceId, runingApp);
            MobileBaseFlow mobileRecordFlow = new MobileBaseFlow(mobileRecordDriver.appiumDriver);
            #endregion

            string recordFilePath = RecordLocatoreService
                .GetRecordFileFullPath(CreateRecordFile.recordFileName);
            await mobileRecordFlow.ClickOnXyUsingFile(recordFilePath);

        }
    }
}
