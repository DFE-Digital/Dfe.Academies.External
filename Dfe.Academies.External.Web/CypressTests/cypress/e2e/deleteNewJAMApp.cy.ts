import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import application from '../page-objects/pages/application'
import confirmApplicationDelete from '../page-objects/pages/confirmApplicationDelete'

describe('Delete application', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.acceptAnalyticsCookies()

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
