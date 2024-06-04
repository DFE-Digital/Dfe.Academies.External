import BasePage from ../basePage
export default class A2BCreateAnAccount extends BasePage {
    static createAccountElementsVisible()
    {
        cy.createAccountElementsVisible()
    }

    static createAccountFailsWithNoData()
    {
        cy.createAccountFailsWithNoData()
    }

    static createAccountSuccessful()
    {

        cy.createAccountSuccessful()
    }
}