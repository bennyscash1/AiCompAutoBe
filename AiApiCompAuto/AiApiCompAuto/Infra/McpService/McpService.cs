using AiApiCompAuto.MobileTest.MobileFlows;
using AiCompAutoBe.MobileTest.MobileFlows;
using ModelContextProtocol.Server;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using SafeCash.Test.ApiTest.InternalApiTest.Buyer;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml;
using static AiCompAutoBe.MobileTest.MobileFlows.MobileAiTaskFlow;

namespace AiApiCompAuto.Infra.McpService;

[McpServerToolType]
public  class McpService
{

    [McpServerTool]
    public async Task<string> McpRunMobileFlow(AndroidDriver mcpDriver, string userGoal)
    {
        string targetText = ExtractTargetFromGoal(userGoal);  // שלב 1: ניתוח הכוונה
        string fullPageSource = mcpDriver.PageSource;
        ///

        MobileAiTaskFlow mobileMcpFlow = new MobileAiTaskFlow(mcpDriver);
        aiResponceTypeEnum aiResponceTypeEnum = await mobileMcpFlow.HandleAiTaskMission(userGoal);
        
        return $"AI Task completed. Final result type: {aiResponceTypeEnum}";
    }

    #region build funciton to my mcp needed


    private static string ExtractTargetFromGoal(string goal)
    {
        // אם יש מספר, תחזיר אותו
        var match = Regex.Match(goal, @"\d+");
        if (match.Success)
            return match.Value;

        // אחרת - נסה מילות מפתח (לדוגמה, "plus" -> "+")
        if (goal.Contains("plus", StringComparison.OrdinalIgnoreCase)) return "+";
        if (goal.Contains("equals", StringComparison.OrdinalIgnoreCase)) return "=";

        return goal; // ברירת מחדל
    }

    public static string? FindClosestElementInXml(string xml, string goalText)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        // מצא את כל האלמנטים עם content-desc או text
        var allNodes = doc.SelectNodes("//*[@content-desc or @text]");
        if (allNodes == null) return null;

        string? bestMatch = null;
        int bestScore = int.MaxValue;

        foreach (XmlNode node in allNodes)
        {
            var text = node.Attributes?["text"]?.Value ?? "";
            var desc = node.Attributes?["content-desc"]?.Value ?? "";

            var combined = text + " " + desc;

            int score = LevenshteinDistance(combined.ToLower(), goalText.ToLower());
            if (score < bestScore)
            {
                bestScore = score;
                bestMatch = combined.Trim();
            }
        }

        return bestScore < 10 ? bestMatch : null;
    }
    public static int LevenshteinDistance(string s, string t)
    {
        var dp = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= t.Length; j++) dp[0, j] = j;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                dp[i, j] = new[] {
                dp[i - 1, j] + 1,      // מחיקה
                dp[i, j - 1] + 1,      // הוספה
                dp[i - 1, j - 1] + cost // החלפה
            }.Min();
            }
        }

        return dp[s.Length, t.Length];
    }

    #endregion




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
