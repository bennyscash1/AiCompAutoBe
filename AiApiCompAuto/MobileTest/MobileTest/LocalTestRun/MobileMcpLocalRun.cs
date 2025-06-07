using AiApiCompAuto.Infra.McpService;
using AiApiCompAuto.MobileTest.MobileFlows;
using AiCompAutoBe.MobileTest.InitalMobile.InitialMobileService;
using AiCompAutoBe.MobileTest.MobileFlows;
using ComprehensiveAutomation.MobileTest.Inital;
using ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows;
using ComprehensivePlayrightAuto.MobileTest.InitalMobile.InitialMobileService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AiCompAutoBe.MobileTest.MobileFlows.MobileAiTaskFlow;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;

namespace AiApiCompAuto.MobileTest.MobileTest.AiPlay
{
    [TestFixture, Category(
        Categories.MobileAndroid),
    Category(TestLevel.Level_1)]
    public class MobileMcpLocalRun
    {
        static string appRuningName = "calculator";

        public async Task<string> RunMcpTask(string goal)
        {
            string deviceId = await new InitialDeviceServices()
               .PrepareTheDeviceToReadyForRun(appRuningName);

            MobileAiDriverFactory mobileDriver = new MobileAiDriverFactory(deviceId, appRuningName);
            //await mobileDriver.InitAndroidAppByAppName(deviceId, appRuningName);

            //MobileMcpFlow mobileFlow = new MobileMcpFlow(mobileDriver.appiumDriver);

            McpService mcpService = new McpService();

            string mcpRecone =  await mcpService.McpRunMobileFlow (mobileDriver.appiumDriver, goal);

            return mcpRecone;
        }
    }
}
