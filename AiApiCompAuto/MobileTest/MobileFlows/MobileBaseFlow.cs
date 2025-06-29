﻿using ComprehensiveAutomation.Test.UiTest.MobileTest.MobilePageObject;
using ComprehensivePlayrightAuto.MobileTest.MobileServices.RecordLocators;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Mac;
using SafeCash.Test.ApiTest.InternalApiTest.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ComprehensiveAutomation.Test.UiTest.MobileTest.MobileFlows
{
    public class MobileBaseFlow
    {
        public AndroidDriver appiumDriver;
        MobileBasePages mobileBasePages;
        MobileLoginPage mobileLoginPage;

        public MobileBaseFlow(AndroidDriver i_driver)
        {
            appiumDriver = i_driver;
            mobileBasePages = new MobileBasePages(i_driver);
            mobileLoginPage = new MobileLoginPage(i_driver);
        }

        public string GetFullPageSource()
        {
            string fullPageSource = appiumDriver.PageSource;
            return fullPageSource;
        }
        #region Talk with ai by text
        public async Task TalkWithApp(string elementView, string inputText ="", string apiKey ="")
        {
            
            By? element = await GetAiElementLocator(elementView, apiKey);
            Assert.That(element != null, $"The element for: '{elementView}' was not found by the AI.  ");

            if (string.IsNullOrEmpty(inputText))
            {
                mobileBasePages.MobileClickElement(element);
            }
            else
            {
                mobileBasePages.MobileInputTextToField(element, inputText);
            }

        }


        #region Get ai element for single element
        private async Task<By?> GetAiElementLocator(string elementView, string apiKey ="")
        {
            mobileBasePages.WaitForPageToLoad();
            string fullPageSource = GetFullPageSource();
            var aiService = new AndroidAiService();

            string locator = "";
            int retry = 0;
            bool isLocatorValid = false;
            bool isElementExsist = false;

            while ((isLocatorValid == false || isElementExsist == false) && retry < 2)
            {
                //Test if the locator is a valid one
                if (!isLocatorValid)
                {
                    locator = await aiService
                        .GetAndroidSingleLocatorFromUserTextInput(fullPageSource, elementView, apiKey);
                    isLocatorValid = AndroidAiService.isLocatorValid(locator);
                }
                if (isLocatorValid)
                {
                    //Test if the locator exsist on the page
                    isElementExsist = mobileBasePages.IsHavyElementFount(By.XPath(locator));
                }
                retry++;
            }
            //Only if the locator is valid and exsist on the page return it
            if (isLocatorValid && isElementExsist)
            {
                return By.XPath(locator);
            }
            Console.WriteLine($"[AI] Could not resolve a valid locator for '{elementView}'. Last attempt: {locator}");
            return null; // or `return By.XPath("");` if your system supports empty XPath
        }
        #endregion

        #region Click on Xy cordinate
        public async Task ClickOnXyUsingFile(string fileCordinatePath)
        {
            RecordLocatoreService recordLocatoreService = new RecordLocatoreService();
            string screenSize = RecordLocatoreService.GetDeviceScreenSizeString();
            var tapPoints = RecordLocatoreService.ExtractTouchCoordinates(fileCordinatePath);

            foreach (var (x, y) in tapPoints)
            {
                mobileBasePages.WaitForPageToLoad();
                mobileBasePages.AdbTap(x, y);
                /*By? aiElement = await GetAiElementLocatorFromXy(fullPageSource,
                    x,y,screenSize);

                mobileBasePages.MobileClickElement(aiElement);*/
            }
        }
        private async Task<By?> GetAiElementLocatorFromXy(string fullSizeScreen,
            int x, int y, string screenSize)
        {
            mobileBasePages.WaitForPageToLoad();
            string fullPageSource = GetFullPageSource();
            var aiService = new AndroidAiService();

            string locator = "";
            int retry = 0;

            while (retry < 2)
            {
                locator = await aiService.GetAndroidLocatorFromUserXyCordinate(fullSizeScreen,
                    x, y, screenSize);
                //Test if the locator valid, of no send it again to the ai
                if (AndroidAiService.isLocatorValid(locator))
                    return By.XPath(locator);
                retry++;
            }

            Console.WriteLine($"[AI] Could not resolve a valid locator for x:'{ x} and y: {y}'cordinate." +
                $" Last attempt: {locator}");
            return null;
        }
        #endregion
        #endregion
        public MobileBaseFlow InitChromeToSearch(bool i_navigateToLogonScreen = true, string i_url = null)
        {
            mobileLoginPage
                .ClickOnUseNoAccount()
               .GeneralScrollDown()
                .ClickOnGotItButton();
            if (i_navigateToLogonScreen)
                appiumDriver.Navigate().GoToUrl(i_url);
            return this;
        }
    }
}
