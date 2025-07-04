package be.hans.cmdb;

import static org.assertj.core.api.Assertions.assertThat;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvSource;

import be.cmdb.helpers.IdentityHelper;
import be.cmdb.model.Identity;
import be.cmdb.pages.LoginPage;
import be.cmdb.pages.MainPage;
import be.cmdb.pages.identity.CreateIdentityPage;
import be.cmdb.pages.identity.EditIdentityPage;
import be.cmdb.pages.identity.IdentityDetailsPage;
import be.cmdb.pages.identity.IdentiyOverviewPage;

/**
 * Test class for Identity operations in the CMDB application.
 */
class IdentityTest extends BaseTest {
    /**
     * Tests the creation of a new identity in the CMDB application.
     * And check if we can find it back
     */
    @Test
    void canCreateNewIdentity() {
        Identity identity = IdentityHelper.createRandomIdentity();
        LoginPage loginPage = new LoginPage(getPage());
        loginPage.navigate();
        MainPage mainPage = loginPage.login("Root", "796724Md");
        boolean isLoggedIn = loginPage.isUserLogedIn();
        assertThat(isLoggedIn).isTrue();
        IdentiyOverviewPage overviewPage = mainPage.openIdentityOverview();
        CreateIdentityPage createPage = overviewPage.openCreateIdentity();
        createPage.setfirstName(identity.getFirstName());
        createPage.setLastName(identity.getLastName());
        createPage.setEmail(identity.getEmail());
        createPage.setCompany(identity.getCompany());
        createPage.setUserId(identity.getUserId());
        createPage.selectType("4");
        createPage.selectLanguage("English");
        createPage.create();

        overviewPage.search(identity.getUserId());
        IdentityDetailsPage detailsPage = overviewPage.openIdentityDetails();
        String logLine = detailsPage.getLastLogline();
        assertThat(logLine).contains(identity.getFirstName())
                          .contains(identity.getLastName())
                          .contains("table identity");
    }

    @DisplayName("Can update an identity for")
    @ParameterizedTest(name="field:{0}, newValue:{1}")
    @CsvSource({
        "UserID,Test123",
        "FirstName,TestFirstName",
        "LastName,TestLastName",
        "EMail,test.test@CMDB.be"
    })
    void canUpdateIdentity(String field, String newValue) {
        String oldValue;
        Identity identity = IdentityHelper.createRandomIdentity(getSession());
        LoginPage loginPage = new LoginPage(getPage());
        loginPage.navigate();
        MainPage mainPage = loginPage.login("Root", "796724Md");
        boolean isLoggedIn = loginPage.isUserLogedIn();
        assertThat(isLoggedIn).isTrue();
        IdentiyOverviewPage overviewPage = mainPage.openIdentityOverview();
        overviewPage.search(identity.getUserId());
        EditIdentityPage editPage = overviewPage.openEditIdentity();
        switch (field) {
            case "UserID":
                oldValue = identity.getUserId();
                editPage.setUserId(newValue);
                break;
            case "FirstName":
                oldValue = identity.getFirstName();
                editPage.setFirstName(newValue);
                break;
            case "LastName":
                oldValue = identity.getLastName();
                editPage.setLastName(newValue);
                break;
            case "EMail":
                oldValue = identity.getEmail();
                editPage.setEmail(newValue);
                break;
            default:
                throw new IllegalArgumentException("Unknown field: " + field);
        }
        editPage.update();

        // Verify that the update was successful
        if (!field.equals("UserId")) {
            overviewPage.search(identity.getUserId());
        } else {
            overviewPage.search(newValue);
        }
        IdentityDetailsPage detailsPage = overviewPage.openIdentityDetails();
        String logLine = detailsPage.getLastLogline();
        assertThat(logLine).contains(newValue)
            .contains("table identity")
            .contains(field)
            .contains(oldValue);
    }
}
