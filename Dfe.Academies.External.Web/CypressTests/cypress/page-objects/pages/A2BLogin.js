import BasePage from "../BasePage"
export default class A2BLogin extends BasePage {
    static login(username, password)
    {
        cy.login(username, password)
    }
}