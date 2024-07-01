﻿using CMDB.Domain.Entities;
using CMDB.UI.Specflow.Abilities.Data;
using CMDB.UI.Specflow.Abilities.Pages.Desktop;
using CMDB.UI.Specflow.Questions.Desktop;
using CMDB.UI.Specflow.Tasks;
using Microsoft.Graph;

namespace CMDB.UI.Specflow.Actors.Desktops
{
    public class DesktopUpdator : DesktopActor
    {
        public DesktopUpdator(ScenarioContext scenarioContext, string name = "DesktopUpdator") : base(scenarioContext, name)
        {
        }
        public async Task<Desktop> CreateDesktop(bool active = true)
        {
            var context = GetAbility<DataContext>();
            return await context.CreateDesktop(admin, active);
        }
        public Desktop UpdateDesktop(Desktop desktop, string field, string value)
        {
            rndNr = rnd.Next();
            var updatePage = Perform(new OpenTheDesktopEditPage());
            updatePage.WebDriver = Driver;
            updatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_editPage");
            switch (field)
            {
                case "Serialnumber":
                    ExpectedLog = GenericLogLineCreator.UpdateLogLine(field, desktop.SerialNumber, value + rndNr.ToString(), admin.Account.UserID, Table); 
                    desktop.SerialNumber = value + rndNr.ToString();
                    updatePage.SerialNumber = desktop.SerialNumber;
                    updatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_SerialNumber");
                    break;
                case "RAM":
                    ExpectedLog = GenericLogLineCreator.UpdateLogLine(field, desktop.RAM, value, admin.Account.UserID, Table);
                    desktop.RAM = value;
                    updatePage.RAM = desktop.RAM;
                    updatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_RAM");
                    break;
                default:
                    log.Fatal($"The update for Field {field} is not implemented");
                    throw new NotImplementedException($"The update for Field {field} is not implemented");
            }
            updatePage.Edit();
            updatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_Edited");
            return desktop;
        }
        public void DeactivateDesktop(Desktop desktop, string reason)
        {
            var deactivatePage = Perform(new OpenTheDesktopDeactivatePage());
            deactivatePage.WebDriver = Driver;
            deactivatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_deactivatePage");
            deactivatePage.Reason = reason;
            deactivatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_Reason");
            deactivatePage.Delete();
            deactivatePage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_Deleted");
            ExpectedLog = GenericLogLineCreator.DeleteLogLine($"Desktop with type {desktop.Type}", admin.Account.UserID, reason, Table);
        }
        public void ActivateDesktop(Desktop desktop)
        {
            var overviewPage = GetAbility<DesktopOverviewPage>();
            overviewPage.Activate();
            overviewPage.TakeScreenShot($"{_scenarioContext.ScenarioInfo.Title}_{_scenarioContext.CurrentScenarioBlock}_Activated");
            ExpectedLog = GenericLogLineCreator.ActivateLogLine($"Desktop with type {desktop.Type}", admin.Account.UserID, Table);
        }
    }
}