using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;

namespace AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay
{
    [TestFixture, Category(
      Categories.MobileAndroid),
          Category(TestLevel.Level_2)]
    public class StopRecordRun
    {
        [Test]
        public async Task _StopRecordRun()
        {
            var recordLocatoreService = new RecordLocatoreService();

            // ✅ Use the stored process
            var process = RecordingSessionStore.CurrentRecordingProcess;

            Thread.Sleep(1000);
            recordLocatoreService.StopAdbRecording(process);

            RecordingSessionStore.CurrentRecordingProcess = null;
        }

    }
}
