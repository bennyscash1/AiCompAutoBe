using ComprehensiveAutomation.Test.Infra.BaseTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ComprehensiveAutomation.Test.UiTest.MobileTest.MobilePageObject
{
    public class MobileLoginPage : MobileBasePages
    {
        private By m_accountIconBy = By.XPath("//android.widget.ImageView[@resource-id='com.google.android.contacts:id/og_apd_internal_image_view']");
        private By m_closeIconBy = By.Id("com.google.android.contacts:id/og_header_close_button");
        private By m_clickApprovePopupPermissionContacs = By.XPath("//android.widget.Button[@resource-id='com.android.permissioncontroller:id/permission_allow_button']");

        //calculator
        

        public MobileLoginPage(AndroidDriver i_driver) : base(i_driver)
        {
            appiumDriver = i_driver;
        }

        /*       public MobileBasePages ClickOnApprovePopupDialogMessage()
               {
                   MobileClickElement(m_clickApprovePopupPermissionContacs);
                   return this;
               }
               public MobileLoginPage ClickOnAccountIcon()
               {
                   MobileClickElement(m_accountIconBy);
                   return this;
               }

               public bool isCloseIconDisplay()
               {
                   WaitForElement(m_closeIconBy);
                   return true;
               }*/
        private By m_iconCalcualtor = By.XPath("//android.widget.ImageButton[@content-desc=\"point\"]");
        private By m_number = By.XPath("//android.widget.ImageButton[@content-desc=\"5\"]");

        public MobileLoginPage ClickOnCalculator(string calculatorNum)
        {
             By m_number = By.XPath($"//android.widget.ImageButton[@content-desc=\"{calculatorNum}\"]");
            MobileClickElement(m_number);
            return this;
        }


        #region Init chrome to login page

        private By m_useWithNoAccount = By.Id("com.android.chrome:id/signin_fre_dismiss_button");
        private By m_IGotItButton = By.XPath("//android.widget.Button[@resource-id=\"com.android.chrome:id/ack_button\"]");

        public MobileLoginPage ClickOnUseNoAccount()
        {
            MobileClickElement(m_useWithNoAccount);
            return this;
        }
        public MobileLoginPage GeneralScrollDown()
        {
            var touch = new PointerInputDevice(PointerKind.Touch);
            var sequence = new ActionSequence(touch, 0);

            sequence.AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, 500, 1800, TimeSpan.Zero));
            sequence.AddAction(touch.CreatePointerDown(0));
            sequence.AddAction(touch.CreatePause(TimeSpan.FromMilliseconds(300)));
            sequence.AddAction(touch.CreatePointerMove(CoordinateOrigin.Viewport, 500, 100, TimeSpan.FromMilliseconds(1000)));
            sequence.AddAction(touch.CreatePointerUp(0));
            Thread.Sleep(1500); // or Task.Delay(1000).Wait();

            appiumChromeDriver.PerformActions(new List<ActionSequence> { sequence });
            Thread.Sleep(1500); // or Task.Delay(1000).Wait();

            return this;
        }
        public MobileLoginPage ClickOnGotItButton()
        {
            MobileClickElement(m_IGotItButton);

            return this;
        }
        #endregion
    }
}
