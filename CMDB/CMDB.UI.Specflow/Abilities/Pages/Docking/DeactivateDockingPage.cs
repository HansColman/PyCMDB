﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Docking
{
    public class DeactivateDockingPage : MainPage
    {
        public DeactivateDockingPage() : base()
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
