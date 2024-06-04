import paths from '../fixtures/accessibilityTestPages.json'
import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'

describe('Check accessibility of all A2B pages', function () {
  it('Checks accessibility', function () {
    const login_username = Cypress.env('LOGIN_USERNAME')
    const login_password = Cypress.env('LOGIN_PASSWORD')

    cy.visit(Cypress.env('URL'))

    cookieHeaderModal.clickAcceptAnalyticsCookies()

    home.clickStartNow()

    login.login(login_username, login_password)

    paths.forEach((link) => {
      cy.visit(Cypress.env('URL') + link)
      cy.excuteAccessibilityTests()
    })
  })
})
