﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Subscription
{
    public class SubscriptionAssignIdentityPage : MainPage
    {
        public SubscriptionAssignIdentityPage(IWebDriver web) : base(web)
        {
        }
        public void SelectIdentity(Domain.Entities.Identity identity)
        {
            SelectValueInDropDownByXpath("//select[@id='Identity']", $"{identity.IdenId}");
        }
    }
}