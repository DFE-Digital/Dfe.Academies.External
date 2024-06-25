import paths from '../fixtures/accessibilityTestPages.json'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'

describe('Check accessibility of all A2B pages', function () {
  beforeEach(() => {
    cy.visit(Cypress.env('URL'))

    home.start()

    login.login()
  })

  it('Checks accessibility', function () {
    paths.forEach((link) => {
      cy.visit(Cypress.env('URL') + link)
      cy.excuteAccessibilityTests()
    })
  })
})
