import BasePage from ../basePage
export default class A2BLogin extends BasePage {
    
    static login(username, password)
    {
        cy.login(username, password)
        // eslint-disable-next-line cypress/no-unnecessary-waiting
        cy.wait(7000)
    }

    static loginToUnauthApplication(username, password)
    {
        cy.loginToUnauthApplication(username, password)
    }

    static createAccount()
    {
        cy.createAccount()
    }

    static forgotPassword()
    {
        cy.forgotPassword()
    }

}
