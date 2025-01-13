﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages.Token;

namespace CMDB.UI.Specflow.Questions.Token
{
    public class OpenTheTokenReleaseIdentityPage : Question<TokenReleaseIdentityPage>
    {
        public override TokenReleaseIdentityPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<TokenDetailPage>();
            page.ClickElementByXpath(Abilities.Pages.MainPage.ReleaseIdenityXpath);
            return new();
        }
    }
}
