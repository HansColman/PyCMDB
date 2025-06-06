﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;

namespace CMDB.UI.Specflow.Questions
{
    public class IsTheUserLoggedIn : Question<bool>
    {
        public override bool PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<MainPage>();
            return page.IsLoggedIn;
        }
    }
}
