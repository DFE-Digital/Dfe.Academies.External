import BasePage from "../BasePage"
export default class CookieHeaderModal extends BasePage {
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