﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;
using CMDB.UI.Specflow.Abilities.Pages.Monitor;

namespace CMDB.UI.Specflow.Questions.Monitor
{
    public class OpenTheMonitorAssignIdentityPage : Question<MonitorAssignIdentityPage>
    {
        public override MonitorAssignIdentityPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<MonitorOverviewPage>();
            page.ClickElementByXpath(MainPage.AssignIdenityXpath);
            page.WaitUntilElmentVisableByXpath("//button[@type='submit']");
            MonitorAssignIdentityPage monitorAssignIdentityPage = WebPageFactory.Create<MonitorAssignIdentityPage>(page.WebDriver);
            return monitorAssignIdentityPage;
        }
    }
}
