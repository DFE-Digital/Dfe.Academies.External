import BasePage from ../basePage
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

    static FAMTrustOpeningDateInputApproverDetailsAndSubmit()
    {
        cy.FAMTrustOpeningDateInputApproverDetailsAndSubmit()
    }
}