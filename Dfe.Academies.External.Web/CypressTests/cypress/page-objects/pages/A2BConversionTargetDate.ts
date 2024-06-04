import BasePage from ../basePage
export default class A2BConversionTargetDate extends BasePage {

    static conversionTargetDateElementsVisible()
    {
        cy.conversionTargetDateElementsVisible()
    }

    static selectConversionTargetDateOptionNo()
    {
        cy.get('#selectoptionNo').click()
        cy.get('#selectoptionNo').should('be.checked')
    }

    static conversionTargetDateSubmit()
    {
        cy.get('input[type=submit]').click()
    }
}