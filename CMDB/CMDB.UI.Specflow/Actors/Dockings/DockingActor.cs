﻿using CMDB.UI.Specflow.Questions.Docking;
using Reqnroll;

namespace CMDB.UI.Specflow.Actors.Dockings
{
    public class DockingActor : CMDBActor
    {
        protected static string Table => "docking";
        public DockingActor(ScenarioContext scenarioContext, string name = "DockingActor") : base(scenarioContext, name)
        {
        }
        public string DockingLastLogLine
        {
            get
            {
                var overviewPAge = Perform(new OpenTheDockingDetailPage());
                overviewPAge.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
                return overviewPAge.GetLastLog();
            }
        }
    }
}
