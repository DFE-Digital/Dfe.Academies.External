import paths from '../fixtures/accessibilityTestPages.json'
import CookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'

describe('Check accessibility of all A2B pages', function () {
  it('Checks accessibility', function () {
    const login_username = Cypress.env('LOGIN_USERNAME')
    const login_password = Cypress.env('LOGIN_PASSWORD')

    cy.visit(Cypress.env('URL'))

    CookieHeaderModal.clickAcceptAnalyticsCookies()

    A2BHome.clickStartNow()

    A2BLogin.login(login_username, login_password)

    paths.forEach((link) => {
      cy.visit(Cypress.env('URL') + link)
      cy.excuteAccessibilityTests()
    })
  })
})
