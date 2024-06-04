import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'
import A2BWhatAreYouApplyingToDo from '../page-objects/pages/A2BWhatAreYouApplyingToDo'
import A2BWhatIsYourRole from '../page-objects/pages/A2BWhatIsYourRole'
import A2BYourApplication from '../page-objects/pages/A2BYourApplication'
import A2BConfirmApplicationDelete from '../page-objects/pages/A2BConfirmApplicationDelete'
import footer from '../page-objects/components/footer'

describe('Delete application', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    A2BHome.homePageElementsVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    A2BHome.clickStartNow()
  })

  it('should be able to delete a JAM application', () => {
    A2BLogin.login(Cypress.env('LOGIN_USERNAME'), Cypress.env('LOGIN_PASSWORD'))

    A2BYourApplications.selectStartANewApplication()

    A2BWhatAreYouApplyingToDo.selectJAMRadioButtonVerifyAndSubmit()

    A2BWhatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit()

    A2BYourApplication.yourApplicationNotStartedElementsVisible()

    // CLICK CANCEL APPLICATION LINK
    A2BYourApplication.selectCancelApplication()

    // VERIFY CONFIRM DELETE APPLICATION PAGE DISPLAYS CORRECTLY
    A2BConfirmApplicationDelete.checkAppIDIsCorrectAndselectConfirmDelete()

    // VERIFY CONFIRMATION OF DELETION BANNER DISPLAYS ON YOUR APPLICATIONS PAGE
    A2BYourApplications.verifyApplicationDeleted()
  })
})
