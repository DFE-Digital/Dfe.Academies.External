import BasePage from "../BasePage"
export default class A2BFAMTrustOpeningDateDetails extends BasePage {
    static selectDayAndInput()
    {
        cy.get('#sip_formtrustopeningdate-day').click()
        cy.get('#sip_formtrustopeningdate-day').type('1')
    }

    static selectMonthAndInput()
    {
        cy.get('#sip_formtrustopeningdate-month').click()
        cy.get('#sip_formtrustopeningdate-month').type('9')
    }

    // BELOW FUNCTION USES LOGIC FROM FIXTURES AND COMMANDS.TS
    static selectYearAndInput()
    {   
        cy.selectYearAndInput()
    }

    static inputApproverDetailsAndSubmit()
    {
        cy.get('#TrustApproverName').click()
        cy.get('#TrustApproverName').type('James Stewart')

        cy.get('#TrustApproverEmail').click()
        cy.get('#TrustApproverEmail').type('james.stewart@aol.com')

        cy.get('input[type="submit"]').should('be.visible').contains('Save and continue').click()

    }
}