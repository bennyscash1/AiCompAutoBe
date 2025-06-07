using NUnit.Framework;
using static ComprehensiveAutomation.Test.Infra.BaseTest.EnumList;

namespace AiApiCompAuto.Infra.McpService
{
    
    [TestFixture, Category(Categories.ApiCategory),
    Category(TestLevel.Level_3)]
    public class McpTest
    {
        [Test]
        public void TestMcpTools()
        {
            var echoResult = McpService.Echo("Hello World");
            Console.WriteLine(echoResult); // Should print: Echo: Hello World

            var smokeTestResult = McpService.RunMobileSmokeTest();
            Console.WriteLine(smokeTestResult); // Should print: 🔧 Smoke test started on mobile device.
        }
    }
}
