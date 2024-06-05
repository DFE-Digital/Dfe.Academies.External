import header from '../page-objects/components/header'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import yourApplication from '../page-objects/pages/yourApplication'
import confirmApplicationDelete from '../page-objects/pages/confirmApplicationDelete'
import footer from '../page-objects/components/footer'

describe('Delete application', () => {
  const applicationId = 100080

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    header.govUkHeaderVisible()
      .applyToBecomeAnAcademyHeaderLinkVisible()

    footer.checkFooterLinksVisible()

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()
  })

  it('should be able to delete a JAM application', () => {
    login.login()

    yourApplications.selectStartANewApplication()

    whatAreYouApplyingToDo.selectJAMRadioButtonVerifyAndSubmit()

    whatIsYourRole.selectChairOfGovernorsRadioButtonVerifyAndSubmit()

    yourApplication.yourApplicationNotStartedElementsVisible()
      .selectCancelApplication()

    confirmApplicationDelete.checkAppIDIsCorrectAndselectConfirmDelete(`${applicationId}`)

    yourApplications.verifyApplicationDeleted(`${applicationId}`)
  })
})
