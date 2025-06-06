using ComprehensiveAutomation.Test.UiTest.MobileTest.MobilePageObject;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows;
using AiApiCompAuto.Infra.McpService;

namespace AiApiCompAuto.MobileTest.MobileFlows
{

    public class MobileMcpFlow : MobileBaseFlow
    {
        MobileLoginPage mobileDriverLocator;
        public MobileMcpFlow(AndroidDriver i_driver) : base(i_driver)
        {
            appiumDriver = i_driver;
            mobileDriverLocator = new MobileLoginPage(appiumDriver);

        }

        public async Task HendleMcpTaskFlow( string userMcpGoal)
        {
            string fullPageSource = GetFullPageSource();
            var mcpService = new McpTools();

            string responceMcp = 
                await mcpService.McpRunMobileFlow(appiumDriver, fullPageSource);
        }
        public async Task HendlMcpActionFlow(string userMcpGoal)
        {
            var mcpService = new McpTools();
            string responceMcp = await mcpService.McpRunMobileFlow(appiumDriver, userMcpGoal);
            // Process the response as needed
        }
    }
}
