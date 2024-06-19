﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.SubscriptionType
{
    public class DeactivateSubscriptionTypePage : MainPage
    {
        public DeactivateSubscriptionTypePage() : base()
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
