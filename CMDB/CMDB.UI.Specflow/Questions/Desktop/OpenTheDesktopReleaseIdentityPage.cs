﻿using Bright.ScreenPlay.Actors;
using CMDB.UI.Specflow.Abilities.Pages.Desktop;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;

namespace CMDB.UI.Specflow.Questions.Desktop
{
    public class OpenTheDesktopReleaseIdentityPage : Question<DesktopReleaseIdentityPage>
    {
        public override DesktopReleaseIdentityPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<DesktopDetailPage>();
            page.ClickElementByXpath(DesktopDetailPage.ReleaseIdenityXpath);
            DesktopReleaseIdentityPage desktopReleaseIdentityPage = WebPageFactory.Create<DesktopReleaseIdentityPage>(page.WebDriver);
            return desktopReleaseIdentityPage;
        }
    }
}
