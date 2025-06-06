using AiApiCompAuto.MobileTest.MobileFlows;
using ModelContextProtocol.Server;
using OpenQA.Selenium.Appium.Android;
using System.ComponentModel;

namespace AiApiCompAuto.Infra.McpService;

[McpServerToolType]
public  class McpTools
{

    [McpServerTool]
    public  async Task<string> McpRunMobileFlow(AndroidDriver mcpDriver, string userGoal)
    {
        var flow = new MobileMcpFlow(mcpDriver);
        string fullPageSource = flow.GetFullPageSource();


        await Task.Delay(100); 
        return "done";
    }




    #region Base example test 

    [McpServerTool, Description("Echoes a message.")]
    public static string Echo(string input) => $"Echo: {input}";

    [McpServerTool, Description("Runs smoke tests on mobile.")]
    public static string RunMobileSmokeTest()
    {
        return "🔧 Smoke test started on mobile device.";
    }
    #endregion

}
