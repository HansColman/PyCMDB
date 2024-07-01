﻿using CMDB.UI.Specflow.Questions.Account;

namespace CMDB.UI.Specflow.Actors.AccountAcctors
{
    public class AccountActor : CMDBActor
    {
        protected string table => "account";
        public AccountActor(ScenarioContext scenarioContext, string name = "CMDB") : base(scenarioContext, name)
        {
        }
        public string AccountLastLogLine
        {
            get
            {
                var detail = Perform(new OpenTheAccountDetailPage());
                detail.WebDriver = Driver;
                detail.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_detail");
                return Perform(new TheAccountDertailLastLogLine());
            }
        }
    }
}
