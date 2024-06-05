import paths from '../fixtures/accessibilityTestPages.json'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'

describe('Check accessibility of all A2B pages', function () {
  it('Checks accessibility', function () {
    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()

    login.login()

    paths.forEach((link) => {
      cy.visit(Cypress.env('URL') + link)
      // TODO add this to interface
      cy.excuteAccessibilityTests()
    })
  })
})
