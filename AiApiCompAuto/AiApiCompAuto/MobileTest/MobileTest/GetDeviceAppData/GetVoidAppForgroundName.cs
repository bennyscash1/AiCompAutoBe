using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;
using static ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService.MobileEmulatorMenegar;

namespace AiApiCompAuto.MobileTest.MobileTest
{
    [TestFixture, Category(
            Categories.MobileAiRun),
        Category(TestLevel.Level_1)]
    public class GetVoidAppForgroundName
    {
        [Test]
        public async Task _GetVoidAppForgroundName()
        {
            string deviceId = new MobileEmulatorMenegar()
             .EnsureDeviceIsRunning(EmulatorEnumList
             .Small_Phone_API_35.ToString());
            string appForground  = GetForegroundAppName();
        }
    }
}
