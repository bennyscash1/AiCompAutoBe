using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
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

namespace AiApiCompAuto.MobileTest.MobileTest.AiPlay.AiRunFromApi.RecordingApi
{
    [TestFixture, Category(
        Categories.MobileAiRun),
    Category(TestLevel.Level_1)]
    public class StopRecordFileApi
    {
        [Test]
        public async Task _StopRecordFileApi(string runingApp, string recordingFileName)
        {
            Process process = new Process();
            RecordLocatoreService recordLocatoreService = new RecordLocatoreService();
            Thread.Sleep(1000);
            recordLocatoreService.StopAdbRecording(process);
        }
    }
}
