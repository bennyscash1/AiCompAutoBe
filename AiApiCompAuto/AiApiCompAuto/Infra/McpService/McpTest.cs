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
            var echoResult = McpTools.Echo("Hello World");
            Console.WriteLine(echoResult); // Should print: Echo: Hello World

            var smokeTestResult = McpTools.RunMobileSmokeTest();
            Console.WriteLine(smokeTestResult); // Should print: 🔧 Smoke test started on mobile device.
        }
    }
}
