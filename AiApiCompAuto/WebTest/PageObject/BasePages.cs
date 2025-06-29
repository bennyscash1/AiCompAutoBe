﻿
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using ComprehensiveAutomation.Test.Infra.BaseTest;
using System.Drawing;
using Microsoft.Playwright;
using AiApiCompAuto.UiTest.BaseData;

namespace AiApiCompAuto.Test.PageObject
{
    public class BasePages : BaseFunctions

    {
        
        public IWebDriver m_driver;
        public IPage pPage;
        public TimeSpan timeOutInSeconds = TimeSpan.FromSeconds(6);
        public By m_okButonInPopupWindowBy = By.XPath("//button[normalize-space()='Ok']");


        public BasePages(IPage i_driver) : base(i_driver)
        {
            pPage = i_driver;
        }




    }
}

