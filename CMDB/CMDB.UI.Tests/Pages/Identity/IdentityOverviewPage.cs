﻿using OpenQA.Selenium;

namespace CMDB.UI.Tests.Pages
{
    public class IdentityOverviewPage : MainPage
    {
        public IdentityOverviewPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public CreateIdentityPage New()
        {
            ClickElementByXpath(NewXpath);
            return new(driver);
        }
        public IdentityDetailPage Detail()
        {
            ClickElementByXpath(InfoXpath);
            return new(driver);
        }
        public UpdateIdentityPage Update()
        {
            ClickElementByXpath(EditXpath);
            WaitUntilElmentVisableByXpath("//input[@name='FirstName']");
            return new(driver);
        }
        public DeactivateIdentityPage Deactivate()
        {
            ClickElementByXpath(DeactivateXpath);
            WaitUntilElmentVisableByXpath("//input[@id='reason']");
            return new(driver);
        }
        public void Activate()
        {
            ClickElementByXpath(ActivateXpath);
            WaitUntilElmentVisableByXpath(NewXpath);
        }
        public AssignAccountPage AssignAccount()
        {
            ClickElementByXpath("//a[@title='Assign Account']");
            WaitUntilElmentVisableByXpath("//button[@type='submit']");
            return new(driver);
        }
        public AssignDevicePage AssignDevice()
        {
            ClickElementByXpath("//a[@title='Assign Device']");
            WaitUntilElmentVisableByXpath("//button[@type='submit']");
            return new(driver);
        }
    }
}