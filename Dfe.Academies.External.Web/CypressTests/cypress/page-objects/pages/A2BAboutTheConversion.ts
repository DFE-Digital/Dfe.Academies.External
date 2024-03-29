import BasePage from "../BasePage"
export default class A2BAboutTheConversion extends BasePage {
    
    static aboutTheConversionNotStartedElementsVisible()
    {
        cy.aboutTheConversionNotStartedElementsVisible()
    }

    static selectContactDetailsStartSection()
    {
        cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()
    }
    
    static aboutTheConversionMainContactsCompleteElementsVisible()
    {
        cy.aboutTheConversionMainContactsCompleteElementsVisible()
    }

    static selectDateForConversionStartSection()
    {
        cy.selectDateForConversionStartSection()
    }

    static aboutTheConversionCompleteElementsVisible()
    {
        cy.aboutTheConversionCompleteElementsVisible()
    }

    static submitAboutTheConversion()
    {
        cy.get('.govuk-button').click()
    }




}