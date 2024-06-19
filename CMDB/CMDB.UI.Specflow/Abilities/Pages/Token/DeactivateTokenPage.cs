﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Token
{
    public class DeactivateTokenPage : MainPage
    {
        public DeactivateTokenPage() : base()
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
