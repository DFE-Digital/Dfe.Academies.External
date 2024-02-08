import BasePage from "../BasePage"
export default class A2BFAMGovernanceStructureSummary extends BasePage {
    static selectStartSection()
    {
        cy.contains('Start section').click()
    }

    static FAMGovernanceStructureSummaryCompleteElementsVisibleAndSubmit()
    {
        cy.get('.govuk-link').eq(1).contains('Change your answers')
        
        cy.get('p').eq(2).contains('fiftyk.docx')
        cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()
           
    }
}