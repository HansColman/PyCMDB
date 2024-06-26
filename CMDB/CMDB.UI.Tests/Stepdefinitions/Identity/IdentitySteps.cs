﻿using CMDB.UI.Tests.Helpers;
using CMDB.UI.Tests.Hooks;
using CMDB.UI.Tests.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.UnitTestProvider;
using Xunit;
using Xunit.Extensions.Ordering;
using helpers = CMDB.UI.Tests.Helpers;
using entity = CMDB.Domain.Entities;
using System.Threading.Tasks;
using CMDB.Testing.Helpers;
using FluentAssertions;

namespace CMDB.UI.Tests.Stepdefinitions
{
    [Binding, Order(1)]
    public sealed class IdentitySteps : TestBase
    {
        private LoginPage login;
        private MainPage main;
        private IdentityOverviewPage overviewPage;
        private CreateIdentityPage createIdentity;
        private UpdateIdentityPage updateIdentity;
        private AssignAccountPage assignAccount;
        private AssignFormPage AssignFom;

        private readonly Random rnd = new();
        private int rndNr;
        private helpers.Identity iden;
        private entity.Account Account;
        private entity.Identity Identity;
        private entity.Device device;
        private string updatedfield, newvalue, reason, expectedlog;
        public IdentitySteps(ScenarioData scenarioData, ScenarioContext context) : base(scenarioData, context)
        {
        }
        #region create
        [Order(1)]
        [Given(@"I want to create an Identity with these details")]
        public void GivenIWantToCreateAnIdentityWithTheseDetails(Table table)
        {
            rndNr = rnd.Next();
            iden = table.CreateInstance<Identity>();
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.IdentityOverview();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Overview");
            createIdentity = overviewPage.New();
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_create");
            createIdentity.FirstName = iden.FirstName + rndNr.ToString();
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_FirstName");
            createIdentity.LastName = iden.LastName + rndNr.ToString();
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_LastName");
            createIdentity.Email = iden.Email;
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Email");
            createIdentity.Company = iden.Company;
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Company");
            createIdentity.UserId = iden.UserId + rndNr.ToString();
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_UserId");
            createIdentity.Type = iden.Type;
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Type");
            createIdentity.Language = iden.Language;
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
        }
        [Order(2)]
        [When(@"I save")]
        public void WhenISave()
        {
            createIdentity.Create();
            createIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Saved");
        }
        [Order(3)]
        [Then(@"I can find the newly created Identity back")]
        public void ThenICanFindTheNewlyCreatedIdenotyBack()
        {
            overviewPage.Search(iden.FirstName + rndNr.ToString());
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            var log = detail.GetLastLog();
            expectedlog = $"The Identity width name: {iden.FirstName + rndNr.ToString()}, {iden.LastName + rndNr.ToString()} is created by {admin.Account.UserID} in table identity";
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        [Order(4)]
        [Given(@"An Identity exisist in the system")]
        public async Task GivenAnIdentityExisistInTheSystem()
        {
            Identity = await context.CreateIdentity(admin);
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.IdentityOverview();
        }
        #region Update
        [Order(5)]
        [When(@"I want to update (.*) with (.*)")]
        public void WhenIWantToUpdateFirstNameWithTestje(string field, string value)
        {
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Overview");
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            rndNr = rnd.Next();
            updateIdentity = overviewPage.Update();
            updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_UpdatePage");
            iden = new()
            {
                FirstName = updateIdentity.FirstName.Trim(),
                LastName = updateIdentity.LastName.Trim(),
                Company = updateIdentity.Company.Trim(),
                UserId = updateIdentity.UserId.Trim(),
                Email = updateIdentity.Email.Trim()
            };
            switch (field)
            {
                case "FirstName":
                    updatedfield = field;
                    newvalue = value + rndNr.ToString();
                    updateIdentity.FirstName = newvalue;
                    updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_FirstName");
                    break;
                case "LastName":
                    updatedfield = field;
                    newvalue = value + rndNr.ToString();
                    updateIdentity.LastName = newvalue;
                    updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_LastName");
                    break;
                case "Company":
                    updatedfield = field;
                    newvalue = value + rndNr.ToString();
                    updateIdentity.Company = newvalue;
                    updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Company");
                    break;
                case "UserID":
                    updatedfield = field;
                    newvalue = value + rndNr.ToString();
                    updateIdentity.UserId = newvalue;
                    updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_UserID");
                    break;
                case "Email":
                    updatedfield = field;
                    newvalue = value;
                    updateIdentity.Email = newvalue;
                    updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EMail");
                    break;
            }
            updateIdentity.Update();
            updateIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Updated");
        }
        [Order(6)]
        [Then(@"The identity is updated")]
        public void ThenTheIdentityIsUpdated()
        {
            switch (updatedfield)
            {
                case "FirstName":
                    expectedlog = $"The {updatedfield} has been changed from {iden.FirstName} to {newvalue} by {admin.Account.UserID} in table identity";
                    overviewPage.Search(newvalue);
                    break;
                case "LastName":
                    expectedlog = $"The {updatedfield} has been changed from {iden.LastName} to {newvalue} by {admin.Account.UserID} in table identity";
                    overviewPage.Search(newvalue);
                    break;
                case "Company":
                    expectedlog = $"The {updatedfield} has been changed from {iden.Company} to {newvalue} by {admin.Account.UserID} in table identity";
                    overviewPage.Search(iden.FirstName);
                    break;
                case "UserID":
                    expectedlog = $"The {updatedfield} has been changed from {iden.UserId} to {newvalue} by {admin.Account.UserID} in table identity";
                    overviewPage.Search(iden.FirstName);
                    break;
                case "Email":
                    expectedlog = $"The {updatedfield} has been changed from {iden.Email} to {newvalue} by {admin.Account.UserID} in table identity";
                    overviewPage.Search(iden.FirstName);
                    break;
            }
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        #region Deactivate
        [Order(7)]
        [Given(@"An acive Identity exisist in the system")]
        public async Task GivenAnAciveIdentityExisistInTheSystem()
        {
            Identity = await context.CreateIdentity(admin);
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.IdentityOverview();
        }
        [Order(8)]
        [When(@"I want to deactivete the identity whith the reason (.*)")]
        public void WhenIWantToDeactiveteTheIdentityWhithTheReasonTest(string reason)
        {
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            this.reason = reason;
            var deactivatepage = overviewPage.Deactivate();
            deactivatepage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_DeactivatePage");
            deactivatepage.Reason = reason;
            deactivatepage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_reason");
            deactivatepage.Delete();
            deactivatepage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Deleted");
        }
        [Order(9)]
        [Then(@"The Idenetity is inactive")]
        public void ThenTheIdenetityIsInactive()
        {
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            int Id = detail.Id;
            entity.Identity iden = context.GetIdentity(Id);
            expectedlog = $"The Identity width name: {iden.Name} is deleted due to {reason} by {admin.Account.UserID} in table identity";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        #region Activate
        [Order(10)]
        [Given(@"An inactive Identity exisist in the system")]
        public async Task GivenAnInactiveIdentityExisistInTheSystem()
        {
            Identity = await context.CreateIdentity(admin, false);
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.IdentityOverview();
        }
        [Order(11)]
        [When(@"I want to activate this identity")]
        public void WhenIWantToActivateThisIdentity()
        {
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            overviewPage.Activate();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Activated");
        }
        [Order(12)]
        [Then(@"The Identity is active")]
        public void ThenTheIdentityIsActive()
        {
            overviewPage.Search(Identity.LastName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            expectedlog = $"The Identity width name: {Identity.Name} is activated by {admin.Account.UserID} in table identity";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        [Given(@"an Account exist as well")]
        public async Task GivenAnAccountExistAsWell()
        {
            Account = await context.CreateAccount(admin);
        }
        #region Assign Account
        [When(@"I assign the account to the identity")]
        public void WhenIAssignTheAccountToTheIdentity()
        {
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            assignAccount = overviewPage.AssignAccount();
            assignAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AssignAccount");
            iden = new()
            {
                UserId = assignAccount.UserId,
                Email = assignAccount.EMail,
                Type = assignAccount.Type,
                FirstName = assignAccount.Name.Split(",")[1].Trim(),
                LastName = assignAccount.Name.Split(",")[0]
            };
            assignAccount.SelectAccount(Account);
            assignAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectIdentity");
            DateTime validFrom = DateTime.Now.AddYears(-1);
            DateTime validUntil = DateTime.Now.AddYears(+1);
            assignAccount.ValidFrom = validFrom;
            assignAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ValidFrom");
            assignAccount.ValidUntil = validUntil;
            assignAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ValidUntil");
            AssignFom = assignAccount.Assign();
            AssignFom.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Assigned");
            Assert.False(assignAccount.IsVaidationErrorVisable());
        }
        [When(@"I fill in the assig form")]
        public void WhenIFillInTheAssigForm()
        {
            string naam = AssignFom.Name;
            Assert.Equal(naam, AssignFom.Employee);
            AssignFom.CreatePDF();
            AssignFom.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_CreatePDF");
        }
        [Then(@"The account is assigned to the idenity")]
        public void ThenTheAccountIsAssignedToTheIdenity()
        {
            overviewPage.Search(iden.UserId);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            int Id = detail.Id;
            entity.Identity identity = context.GetIdentity(Id);
            expectedlog = $"The Identity with name: {identity.FirstName}, {identity.LastName} is assigned to Account with UserID: {Account.UserID} by {admin.Account.UserID} in table identity";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog,"The log should match");
        }
        #endregion
        #region release account
        [Given(@"The account is assigned to the Idenitity")]
        public async Task GivenTheAccountIsAssignedToTheIdenitity()
        {
            await context.AssignIden2Account(Identity, Account,admin) ;
        }
        [When(@"I release the account from my Identity")]
        public void WhenIReleaseTheAccountFromMyIdentity()
        {
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
        }
        [When(@"I fill in the release account form for my Identity")]
        public void WhenIFillInTheReleaseAccountFormForMyIdentity()
        {
            IdentityDetailPage detailPage = overviewPage.Detail();
            detailPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Deatil");
            ReleaseAccountPage releasePage = detailPage.ReleaseAccount();
            releasePage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ReleasePage");
            releasePage.Title.Should().BeEquivalentTo("Release account from identity", "Title should be correct");
            releasePage.ITEmployee.Should().BeEquivalentTo(admin.Account.UserID, "The IT employee should be the admin");
            releasePage.Employee.Should().BeEquivalentTo(Identity.Name, "The employee should be the name of the identity");
            releasePage.CreatePDF();
            releasePage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_PDF Created");
        }
        [Then(@"The account is released")]
        public void ThenTheAccountIsReleased()
        {
            expectedlog = $"The Identity with name: {Identity.Name} is released from Account with UserID: {Account.UserID} in application {Account.Application.Name} by {admin.Account.UserID} in table identity";
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            var GetLastlog = detail.GetLastLog();
            GetLastlog.Should().BeEquivalentTo(expectedlog, "The log should match");
        }
        #endregion
        [Given(@"a (.*) exist as well")]
        public async Task GivenALaptopExistAsWell(string category)
        {
            switch (category)
            {
                case "Laptop":
                    device = await context.CreateLaptop(admin);
                    break;
                case "Desktop":
                    device = await context.CreateDesktop(admin);
                    break;
                case "Token":
                    device = await context.CreateToken(admin);
                    break;
                case "Monitor":
                    device = await context.CreateMonitor(admin);
                    break;
                case "Docking":
                    device = await context.CreateDocking(admin);
                    break;
            }
            TestData.Add("device", device);
        }
        #region Assign Devices
        [When(@"I assign that (.*) to the identity")]
        public void WhenIAssignThatLaptopToTheIdentity(string category)
        {
            overviewPage.Search(Identity.FirstName);
            var assignPage = overviewPage.AssignDevice();
            assignPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AssignDevice");
            assignPage.ClickDevice(device);
            AssignFom = assignPage.Assign();
        }
        [Then(@"The (.*) is assigned")]
        public void ThenTheLaptopIsAssigned(string category)
        {
            log.Debug($"Check if device of type: {category} is assigned");
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            string expectedlog = $"The Identity with name: {Identity.FirstName}, {Identity.LastName} is assigned to {device.Category.Category} " +
                        $"with {device.AssetTag} by {admin.Account.UserID} in table identity";
            var GetLastlog = detail.GetLastLog();
            GetLastlog.Should().BeEquivalentTo(expectedlog, "The log should match");
        }
        #endregion
        #region Release device
        [Given(@"The (.*) is assigned to the Identity")]
        public async Task AssignDevice2Identity(string category)
        {
            device = (entity.Device)TestData.Get("device");
            log.Debug($"Assign device of type: {category} to my idenity {Identity.Name}");
            await context.AssignIdentity2Device(admin, device, Identity);
        }
        [When(@"I release the (.*) from the Identity")]
        public void ReleaseDevice(string category)
        {
            log.Debug($"Release device of type: {category} is assigned");
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
        }
        [When(@"I fill in the release form for my Identity")]
        public void FillinReleaseForm()
        {
            IdentityDetailPage detailPage = overviewPage.Detail();
            detailPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Deatil");
            ReleaseDevicePage releasePage = detailPage.ReleaseDevice();
            releasePage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ReleasePage");
            releasePage.Title.Should().BeEquivalentTo("Release device from identity", "Title should be correct");
            releasePage.ITEmployee.Should().BeEquivalentTo(admin.Account.UserID, "The IT employee should be the admin");
            releasePage.Employee.Should().BeEquivalentTo(Identity.Name, "The employee should be the name of the identity");
            releasePage.CreatePDF();
            releasePage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_PDF Created");
        }
        [Then(@"The (.*) is released from the Identity")]
        public void CheckReleased(string category)
        {
            log.Debug($"Check if device of type: {category} is Released");
            expectedlog = $"The identity with name: {Identity.Name} is released from {device.Category.Category} with {device.AssetTag} by {admin.Account.UserID} in table identity";
            overviewPage.Search(Identity.FirstName);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Searched");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
            var GetLastlog = detail.GetLastLog();
            GetLastlog.Should().BeEquivalentTo(expectedlog, "The log should match");
        }
        #endregion
    }
}
