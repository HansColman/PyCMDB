﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Types
{
    public class DeactivateTypePage : MainPage
    {
        public DeactivateTypePage(IWebDriver web) : base(web)
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
