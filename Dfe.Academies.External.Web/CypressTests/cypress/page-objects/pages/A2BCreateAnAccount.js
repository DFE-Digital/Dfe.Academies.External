import BasePage from "../BasePage"
import DataGenerator from "../../fixtures/data-generator"
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