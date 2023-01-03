import BasePage from "../BasePage"
import DataGenerator from "../../fixtures/data-generator"
export default class A2BCreateAnAccountConfirm extends BasePage {
    static createAccountConfirmElementsVisible()
    {
        cy.createAccountConfirmElementsVisible()
    }
}