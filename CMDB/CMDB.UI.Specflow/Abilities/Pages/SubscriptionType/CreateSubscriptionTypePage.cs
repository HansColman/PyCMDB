﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.SubscriptionType
{
    public class CreateSubscriptionTypePage : MainPage
    {
        public CreateSubscriptionTypePage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public string Category
        {
            set => SelectTektInDropDownByXpath("//select[@id='Category']", value);
        }
        public string Provider
        {
            set => EnterInTextboxByXPath("//input[@id='Provider']", value);
        }
        public string Type
        {
            set => EnterInTextboxByXPath("//input[@id='Type']", value);
        }
        public string Description
        {
            set => EnterInTextboxByXPath("//input[@id='Description']", value);
        }
        public void Create()
        {
            ClickElementByXpath("//button[.='Create']");
        }
    }
}
