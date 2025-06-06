﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Identity
{
    public class IdentityOverviewPage : MainPage
    {
        public IdentityOverviewPage(IWebDriver web) : base(web)
        {
        }
        public void Activate()
        {
            ClickElementByXpath(ActivateXpath);
            WaitUntilElmentVisableByXpath(NewXpath);
        }
    }
}