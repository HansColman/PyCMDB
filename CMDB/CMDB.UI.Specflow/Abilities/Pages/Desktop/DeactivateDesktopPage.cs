﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Desktop
{
    public class DeactivateDesktopPage : MainPage
    {
        public DeactivateDesktopPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public string Reason
        {
            set => EnterInTextboxByXPath("//input[@id='reason']", value);
        }
        public void Delete()
        {
            ClickElementByXpath("//button[.='Delete']");
        }
    }
}
