﻿namespace CMDB.UI.Specflow.Abilities.Pages.Identity
{
    public class ReleaseSubscriptionPage : MainPage
    {
        public string Employee
        {
            set => EnterInTextboxByXPath("//input[@id='Employee']", value);
            get => TekstFromTextBox("//input[@id='Employee']");
        }
        public string ITEmployee
        {
            set => EnterInTextboxByXPath("//input[@id='ITEmp']", value);
            get => TekstFromTextBox("//input[@id='ITEmp']");
        }
    }
}
