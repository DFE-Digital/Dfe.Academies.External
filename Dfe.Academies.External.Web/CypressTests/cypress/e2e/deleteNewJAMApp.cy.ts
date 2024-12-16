import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import application from '../page-objects/pages/application'
import confirmApplicationDelete from '../page-objects/pages/confirmApplicationDelete'

describe('Delete application', () => {

  const homeAssertions = {
    warningIcon: () => cy.get('span[class="govuk-warning-text__icon"]'),
    warningText: () => cy.get('strong[class="govuk-warning-text__text"]'),
  }

  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

    homeAssertions.warningIcon().should('not.exist')

    homeAssertions.warningText().should('not.exist')

    home.start()

    login.login()
  })

  it('should be able to delete a JAM application', () => {
    
    cy.executeAccessibilityTests()

    yourApplications.startNewApplication()

    cy.executeAccessibilityTests()

    whatAreYouApplyingToDo.startApplication('Join A MAT')

    cy.executeAccessibilityTests()

    whatIsYourRole.chooseRole('Governor')

    cy.executeAccessibilityTests()

    application.selectCancelApplication()

    cy.executeAccessibilityTests()

    confirmApplicationDelete.confirmDelete()

    cy.executeAccessibilityTests()

    yourApplications.verifyApplicationDeleted()
    
  })
})
