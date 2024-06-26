﻿using helpers = CMDB.UI.Tests.Helpers;
using entity = CMDB.Domain.Entities;
using CMDB.UI.Tests.Hooks;
using CMDB.UI.Tests.Pages;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using CMDB.Testing.Helpers;
using FluentAssertions;

namespace CMDB.UI.Tests.Stepdefinitions
{
    [Binding]
    public sealed class AccountSteps : TestBase
    {
        private LoginPage login;
        private MainPage main;
        private AccountOverviewPage overviewPage;
        private CreateAccountPage createAccount;
        private AssignFormPage AssignFom;
        private AccountReleaseIdentityPage releaseIdentity;

        private readonly Random rnd = new();
        private int rndNr;
        private helpers.Account account;
        private entity.Account Account;
        private entity.Identity Identity;

        private string expectedlog, oldValue, newValue, changedField;

        public AccountSteps(ScenarioData scenarioData, ScenarioContext context) : base(scenarioData, context)
        {
        }
        #region Create
        [Given(@"I want to create a Account with the following details")]
        public void GivenIWantToCreateAAccountWithTheFollowingDetails(Table table)
        {
            account = table.CreateInstance<helpers.Account>();
            rndNr = rnd.Next();
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.AccountOverview();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AccountOverview");
            createAccount = overviewPage.New();
            createAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ClickNew");
            createAccount.UserId = account.UserId + rndNr.ToString();
            createAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterUserId");
            createAccount.Type = account.Type;
            createAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectType");
            createAccount.Application = account.Application;
            createAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectAppliction");
        }
        [When(@"I save the account")]
        public void WhenISaveTheAccount()
        {
            createAccount.Create();
            createAccount.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Saved");
        }
        [Then(@"The account is saved")]
        public void ThenTheAccountIsSaved()
        {
            overviewPage.Search(account.UserId + rndNr.ToString());
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            var log = detail.GetLastLog();
            expectedlog = $"The Account width UserID: {account.UserId + rndNr.ToString()} with type {account.Type} for application {account.Application} is created by {admin.Account.UserID} in table account";
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        [Given(@"There is an account existing")]
        public async Task GivenThereIsAnAccountExisting()
        {
            Account = await context.CreateAccount(admin);
            rndNr = rnd.Next();
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.AccountOverview();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AccountOverview");
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
        }
        #region update
        [When(@"I change the (.*) to (.*) and I save the changes")]
        public void WhenIChangeTheUserIdToTestjeAndISaveTheChanges(string field, string newValue)
        {
            var editPage = overviewPage.Edit();
            editPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EditAccount");
            changedField = field;
            switch (field)
            {
                case "UserId":
                    oldValue = editPage.UserId;
                    this.newValue = newValue + rndNr.ToString();
                    editPage.UserId = this.newValue;
                    editPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ChangedUserId");
                    break;
                case "Type":
                    oldValue = "Normal User";
                    this.newValue = newValue;
                    editPage.Type = newValue;
                    
                        editPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ChangedType");
                    break;
                case "Application":
                    oldValue = "CMDB";
                    this.newValue = newValue;
                    editPage.Application = newValue;
                    editPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_ChangedApplication");
                    break;
            }
            editPage.Edit();
            editPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Changed");
        }
        [Then(@"The changes in account are saved")]
        public void ThenTheChangesInAccountAreSaved()
        {
            switch (changedField)
            {
                case "UserId":
                    overviewPage.Search(newValue);
                    expectedlog = $"The UserId has been changed from {oldValue} to {newValue} by {admin.Account.UserID} in table account";
                    break;
                case "Type":
                    overviewPage.Search(Account.UserID);
                    expectedlog = $"The Type has been changed from {oldValue} to {newValue} by {admin.Account.UserID} in table account";
                    break;
                case "Application":
                    overviewPage.Search(Account.UserID);
                    expectedlog = $"The Application has been changed from {oldValue} to {newValue} by {admin.Account.UserID} in table account";
                    break;
            }
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        #region deactivate
        [Given(@"There is an active account existing")]
        public async Task GivenThereIsAnActiveAccountExisting()
        {
            Account = await context.CreateAccount(admin);
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID); 
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.AccountOverview();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AccountOverview");
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
        }
        [When(@"I deactivate the account with reason (.*)")]
        public void WhenIDeactivateTheAccountWithReasonTest(string reason)
        {
            var deactivatepage = overviewPage.Deactivate();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Deactivate");
            deactivatepage.Reason = reason;
            newValue = reason;
            deactivatepage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterReason");
            deactivatepage.Delete();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Deactivated");
        }
        [Then(@"the account is inactive")]
        public void ThenTheAccountIsInactive()
        {
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            var accId = detail.Id;
            entity.Account account = context.GetAccount(accId);
            expectedlog = $"The Account width UserID: {account.UserID} and type {account.Type.Description} is deleted due to {newValue} by {admin.Account.UserID} in table account";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        #region Activate
        [Given(@"There is an inactive account existing")]
        public async Task GivenThereIsAnInactiveAccountExisting()
        {
            Account = await context.CreateAccount(admin, false);
            ScenarioData.Driver.Navigate().GoToUrl(Settings.Url);
            login = new LoginPage(ScenarioData.Driver);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Start");
            login.EnterUserID(admin.Account.UserID);
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectUser");
            login.EnterPassword("1234");
            login.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterPwd");
            main = login.LogIn();
            main.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Logedin");
            overviewPage = main.AccountOverview();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AccountOverview");
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
        }
        [When(@"I activate the account")]
        public void WhenIActivateTheAccount()
        {
            overviewPage.Activate();
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Activated");
        }
        [Then(@"The account is active")]
        public void ThenTheAccountIsActive()
        {
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            var accId = detail.Id;
            entity.Account account = context.GetAccount(accId);
            expectedlog = $"The Account width UserID: {account.UserID} and type {account.Type.Description} is activated by {admin.Account.UserID} in table account";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
        #region Assign iden to acc
        [When(@"I assign the identity to my account")]
        public void WhenIAssignTheIdentityToMyAccount()
        {
            Identity = (entity.Identity)TestData.Get("Identity");
            var assign = overviewPage.AssignIdentity();
            assign.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AssignIdentity");
            assign.SelectIdentity(Identity);
            assign.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_SelectIdentity");
            DateTime validFrom = DateTime.Now.AddYears(-1);
            DateTime validUntil = DateTime.Now.AddYears(+1);            
            assign.ValidFrom = validFrom;
            assign.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterValidFrom");
            assign.ValidUntil = validUntil;
            assign.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_EnterValidEnt");
            AssignFom = assign.Assign();
            AssignFom.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_AssignForm");
            Assert.False(AssignFom.IsVaidationErrorVisable());
        }
        [When(@"I fill in the assig form for my account")]
        public void WhenIFillInTheAssigForm()
        {
            string naam = AssignFom.Name;
            Assert.Equal(naam, AssignFom.Employee);
            AssignFom.CreatePDF();
            AssignFom.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_PDFCreated");
        }
        [Then(@"The identity is assigned to my account")]
        public void ThenTheIdentityIsAssignedToMyAccount()
        {
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            expectedlog = $"The Account with UserID: {Account.UserID} is assigned to Identity with name: {Identity.Name} by {admin.Account.UserID} in table account";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog, "Log should match");
        }
        #endregion
        #region release account
        [Given(@"There is an Identity assigned")]
        public async Task GivenThereIsAnIdentityAssigned()
        {
            Identity = await context.CreateIdentity(admin);
            await context.AssignIden2Account(Identity, Account, admin);
        }
        [When(@"I release the Identity")]
        public void WhenIReleaseTheIdentity()
        {
            var detailPage = overviewPage.Detail();
            detailPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            releaseIdentity = detailPage.ReleaseIdentity();
        }
        [When(@"I fill in the release form")]
        public void WhenIFillInTheReleaseForm()
        {
            releaseIdentity.ITEmployee.Should().BeEquivalentTo(admin.Account.UserID);
            releaseIdentity.IdentityName.Should().BeEquivalentTo(Identity.Name);
            releaseIdentity.CreatePDF();
            releaseIdentity.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
        }
        [Then(@"The identity is released from my account")]
        public void ThenTheIdentityIsReleasedFromMyAccount()
        {
            overviewPage.Search(Account.UserID);
            overviewPage.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Search");
            var detail = overviewPage.Detail();
            detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_Detail");
            expectedlog = $"The Account with UserID: {Account.UserID} is released from Identity with name: {Identity.Name} by {admin.Account.UserID} in table account";
            var log = detail.GetLastLog();
            log.Should().BeEquivalentTo(expectedlog);
        }
        #endregion
    }
}
