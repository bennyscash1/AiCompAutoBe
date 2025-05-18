using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using SafeCash.Test.ApiTest.Integration.OpenAi;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace SafeCash.Test.ApiTest.InternalApiTest.Buyer
{
    public class AndroidAiService 
    {
         public async Task <string> GetAndroidSingleLocatorFromUserTextInput(
             string fullPageSource, string userInputView)
        {          
            OpenAiService openAiService = new OpenAiService();
            string responceLocatorFromAi = await openAiService.GetClaudeResponse(
                $"Here is the full app XML source:," +
                $"{fullPageSource}\n\n" +
                $" I need to find the XPath locator for the button or input field for the next line>>: '\n"+
                $"{userInputView}'\n\n"+
                $"Please return only xpath without any other text",
                OpenAiService.SystemPromptTypeEnum.MobileTextInpueRequest);
           bool isLocatorValid = AndroidAiService.isLocatorValid(responceLocatorFromAi);
            if (isLocatorValid)
            {
                return responceLocatorFromAi;
            }
            else
            {
                // If the locator is not valid, you can handle it here
                Console.WriteLine($"Invalid XPath locator: {responceLocatorFromAi}");
                return string.Empty; // or throw an exception, or return a default value
            }   
        }
        public async Task<string> GetAndroidLocatorFromUserXyCordinate(
            string fullPageSource, int x, int y, string screenSize)
        {
            OpenAiService openAiService = new OpenAiService();
            string responceLocatorFromAi = await openAiService.OpenAiServiceRequest(
                $"Here is the full app XML source:," +
                $"{fullPageSource}\n\n" +
                $" I need to find the XPath locator for the button or input field according to the X and Y cordinate>>: '\n" +
                $"the X cordinate:{x}', the Y cordinate: {y}\n\n" +
                $"Please return only xpath without any other text",
                OpenAiService.SystemPromptTypeEnum.MobileXyCordinateRequest);
            bool isLocatorValid = AndroidAiService.isLocatorValid(responceLocatorFromAi);
            if (isLocatorValid)
            {
                return responceLocatorFromAi;
            }
            else
            {
                // If the locator is not valid, you can handle it here
                Console.WriteLine($"Invalid XPath locator: {responceLocatorFromAi}");
                return string.Empty; // or throw an exception, or return a default value
            }
        }
        public static bool isLocatorValid(string locator)
        {
            if (string.IsNullOrWhiteSpace(locator))
                return false;

            // Basic check: should start with "/" or "(" for XPath
            if (!locator.TrimStart().StartsWith("/"))
                return false;

            // Try to load it as an XPath using XmlDocument and XPathNavigator
            try
            {
                var dummyXml = "<root><android.widget.ImageButton content-desc=\"5\" /></root>";
                var xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(dummyXml);
                var nav = xmlDoc.CreateNavigator();
                var nodes = nav.Select(locator);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsImagesAreCompareUseAi(string expectedImagePath, string actualImagePath)
        {
            OpenAiService openAiService = new OpenAiService();
            string responseOpenAi = await openAiService.OpenAiServiceRequest(
                $"image 1: {expectedImagePath} " +
                $"image 2: {actualImagePath} " ,
                OpenAiService.SystemPromptTypeEnum.ImagesCompare);

            if (responseOpenAi.Contains("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Ai service for API tasks 
        public async Task<string> GetAiResponedTaskAsJson(
          string fullPageSource, string userEndGoalMission, string userUpdateOnFailedScenario = "")
        {
            OpenAiService openAiService = new OpenAiService();
            string lastResponse = string.Empty;

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                string userPrompt =
                    $"The user Goal task:\n{userEndGoalMission}\n\n" +
                    $"Here the XML:\n{fullPageSource}\n\n" +
                    $"{userUpdateOnFailedScenario}";

                if (attempt > 1)
                {
                    userPrompt +=
                        $"Your previous response was invalid:\n{lastResponse}\n\n" +
                        "⚠️ Please return a **valid JSON only**, one of the following:\n" +
                        "- { \"type\": 1, \"xpath\": \"...\" }\n" +
                        "- { \"type\": 2, \"xpath\": \"...\", \"value\": \"...\" }\n" +
                        "- { \"type\": 3 }\n" +
                        "- { \"type\": 0 }\n\n" +
                        "If the task is already complete, return only { \"type\": 3 }.";
                }

                lastResponse = await openAiService.GrokRequestService(
                    userPrompt,
                    OpenAiService.SystemPromptTypeEnum.MobileSystemPromptMissionTask
                );

                if (IsAiReturnValidJson(lastResponse, out var cleanJson))
                    return cleanJson;
            }

            // After 3 failed attempts, throw to stop test and alert the API controller
            throw new InvalidOperationException(
                "AI failed to return a valid JSON response after 3 attempts.\nLast response:\n" + lastResponse);
        }


        public static bool IsAiReturnValidJson(string input, out string fixedJson)
        {
            fixedJson = null;

            // Try to locate the first valid JSON object in the input
            int start = input.IndexOf('{');
            int end = input.LastIndexOf('}');
            if (start == -1 || end == -1 || end <= start)
                return false;

            string jsonCandidate = input.Substring(start, end - start + 1).Trim();

            try
            {
                using (JsonDocument.Parse(jsonCandidate))
                {
                    fixedJson = jsonCandidate;
                    return true;
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("The AI responded with an invalid JSON.");
                return false;
            }
        }

        #endregion

    }
}

