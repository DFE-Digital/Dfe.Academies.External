import BasePage from "../BasePage"
export default class A2BFAMKeyPeopleSummary extends BasePage {
    static selectAddKeyPerson()
    {
        cy.contains('Add a key person').click()
    }

    static FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit()
    {
        cy.get('.govuk-heading-s').eq(0).contains('James Smith (Financial director)')
        cy.get('.app-task-list__item').contains('15/10/1980')
        cy.get('.app-task-list__item').contains('Please provide a biography of them')
        cy.get('.app-task-list__item').contains('Edit')

        cy.get('.govuk-button').eq(0).should('be.visible').contains('Add a key person')

        cy.get('.govuk-button').eq(1).should('be.visible').contains('Save and return to your application').click()


    }


}