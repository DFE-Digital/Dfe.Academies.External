import BasePage from "../BasePage"
export default class A2BFAMKeyPersonDetails extends BasePage {
    static fillKeyPersonDetailsAndSubmit()
    {
        cy.get('#TrustKeyPersonName').click()
        cy.get('#TrustKeyPersonName').type('James Smith')

        cy.get('#TrustKeyPersonFinancialDirector').click()

        cy.get('#TrustKeyPersonTimeInRole').click()
        cy.get('#TrustKeyPersonTimeInRole').type('35 hours p/w')

        cy.get('#sip_formtrustkeypersondate-day').click()
        cy.get('#sip_formtrustkeypersondate-day').type('15')

        cy.get('#sip_formtrustkeypersondate-month').click()
        cy.get('#sip_formtrustkeypersondate-month').type('10')

        cy.get('#sip_formtrustkeypersondate-year').click()
        cy.get('#sip_formtrustkeypersondate-year').type('1980')

        cy.get('#TrustKeyPersonBiography').click()
        cy.get('#TrustKeyPersonBiography').type('Please provide a biography of them')

        cy.get('input[type="submit"]').should('be.visible').contains('Save and continue').click()
    }
}