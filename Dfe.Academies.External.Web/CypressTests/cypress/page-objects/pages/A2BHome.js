import BasePage from "../BasePage"
export default class A2BHome extends BasePage {

    static StartNowVisible() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').should('be.visible')
    }

    static clickStartNow() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').click({force: true})
    }
}