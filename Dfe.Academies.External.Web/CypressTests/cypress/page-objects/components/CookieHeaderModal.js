export default class NavBar {
    static clickAcceptAnalyticsCookies() {
        cy.get('[value="accept"]').click()
    }

    static clickRejectAnalyticsCookies() {
        cy.get('[value="reject"]').click()
    }

    static clickViewCookies() {
        cy.get('.govuk-button-group > .govuk-link').click()
    }



}