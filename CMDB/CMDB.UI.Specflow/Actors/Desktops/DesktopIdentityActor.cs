﻿using CMDB.Domain.Entities;
using CMDB.UI.Specflow.Abilities.Data;
using CMDB.UI.Specflow.Questions.DataContextAnswers;
using CMDB.UI.Specflow.Questions.Desktop;
using CMDB.UI.Specflow.Tasks;

namespace CMDB.UI.Specflow.Actors.Desktops
{
    public class DesktopIdentityActor : DesktopUpdator
    {
        public DesktopIdentityActor(ScenarioContext scenarioContext, string name = "DesktopUpdator") : base(scenarioContext, name)
        {
        }
        public async Task<Identity> CreateNewIdentity()
        {
            return await Perform(new CreateTheIdentity());
        }
        public async Task AssignDesktop2Identity(Desktop desktop, Identity identity)
        {
            try
            {
                var context = GetAbility<DataContext>();
                await context.AssignIdentity2Device(admin, desktop, identity);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
        public void AssignTheIdentity2Desktop(Desktop desktop, Identity identity)
        {
            ExpectedLog = GenericLogLineCreator.AssingDevice2IdenityLogLine($"Desktop with {desktop.AssetTag}",
                $"Identity with name: {identity.Name}", admin.Account.UserID, Table);
            var page = Perform(new OpenTheDesktopAssignIdentityPage());
            page.WebDriver = Driver;
            page.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AssignIdentityPage");
            page.SelectIdentity(identity);
            page.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectedIdentity");
        }
        public void FillInAssignForm(Identity identity)
        {
            var assignForm = OpenAssignFom();
            assignForm.ITEmployee.Should().BeEquivalentTo(admin.Account.UserID, "The IT employee should be the admin");
            assignForm.Employee.Should().BeEquivalentTo(identity.Name, "The employee should be the name of the identity");
            Perform<ClickTheGeneratePDFOnAssignForm>();
            assignForm.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Assigned");
        }
        public void ReleaseIdentity(Desktop desktop, Identity identity)
        {
            ExpectedLog = GenericLogLineCreator.ReleaseDeviceFromIdentityLogLine($"Desktop with {desktop.AssetTag}",
                $"Identity with name: {identity.Name}", admin.Account.UserID, Table);
            var detailPage = Perform(new OpenTheDesktopDetailPage());
            detailPage.WebDriver = Driver;
            detailPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_DetailPage");
            var page = Perform(new OpenTheDesktopReleaseIdentityPage());
            page.WebDriver = Driver;
            page.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ReleaseIdentityPage");
            page.ITEmployee.Should().BeEquivalentTo(admin.Account.UserID, "The IT employee should be the admin");
            page.Employee.Should().BeEquivalentTo(identity.Name, "The employee should be the name of the identity");
            page.CreatePDF();
        }
    }
}
