import BasePage from "../BasePage"
export default class A2BConversionTargetDate extends BasePage {

    static conversionTargetDateElementsVisible()
    {
        cy.conversionTargetDateElementsVisible()
    }

    static conversionTargetDateSubmit()
    {
        cy.get('input[type="submit"]').click()
    }
}