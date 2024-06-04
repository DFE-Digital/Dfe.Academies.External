import BasePage from ../basePage
export default class A2BFAMTrustname extends BasePage {
    static FAMEnterTrustnameAndSubmit()
    {
        cy.get('#ProposedNameOfTrust').click()
        cy.get('#ProposedNameOfTrust').type('Plymouth')
        cy.get('input[type=submit]').click()
    }
}