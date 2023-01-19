import BasePage from "../BasePage"
export default class Header extends BasePage {

    static govUkHeaderVisible() {
        cy.get('.govuk-header__logotype').should('be.visible').contains('GOV.UK')
    }

    static applyToBecomeAnAcademyHeaderLinkVisible() {
        cy.get('.govuk-header__content').contains('Apply to become an Academy')
    }

}