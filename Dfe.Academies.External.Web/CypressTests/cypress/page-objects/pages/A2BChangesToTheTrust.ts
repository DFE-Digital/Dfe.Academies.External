import BasePage from "../BasePage"
export default class A2BChangesToTheTrust extends BasePage {
    static changesToTheTrustElementsVisible()
    {
        cy.changesToTheTrustElementsVisible()
    }

    changesToTheTrustClickYesEnterChangesAndSubmit()
    {
        cy.get('#revenueTypeYes').click()
        cy.enterChangesToTheTrust()
        cy.get('input[type="submit"]').click()
    }
}