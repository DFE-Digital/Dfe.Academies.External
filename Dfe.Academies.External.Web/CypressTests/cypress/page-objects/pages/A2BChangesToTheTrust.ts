import BasePage from "../BasePage"
export default class A2BChangesToTheTrust extends BasePage {
    static changesToTheTrustElementsVisible()
    {
        cy.changesToTheTrustElementsVisible()
    }

    static changesToTheTrustClickYes()
    {
        cy.get('#revenueTypeYes').click()
    }

    static enterChangesToTheTrust()
    {
        cy.enterChangesToTheTrust()
    }

    static changesToTheTrustSubmit()
    {
        cy.get('input[type="submit"]').click()
    }
}