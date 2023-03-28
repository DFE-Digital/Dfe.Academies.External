import BasePage from "../BasePage"
export default class A2BYourApplication extends BasePage {
    
    static yourApplicationNotStartedElementsVisible()
    {
        cy.yourApplicationNotStartedElementsVisible()
    }
    
    static yourApplicationNotStartedButSchoolAddedElementsVisible()
    {
        cy.yourApplicationNotStartedButSchoolAddedElementsVisible()
    }

    static yourApplicationNotStartedButTrustSectionCompleteElementsVisible()
    {
        cy.yourApplicationNotStartedButTrustSectionCompleteElementsVisible()
    }

    static selectAddATrust()
    {
        cy.selectAddATrust()
    }

    static selectAddASchool()
    {
        cy.selectAddASchool()
    }

    static selectTrustDetails() 
    {
        cy.selectTrustDetails()
    }

    static selectChangeSchool() 
    {
        cy.selectChangeSchool()
    }

    static selectChangeTrust() 
    {
        cy.selectChangeTrust()
    }

    static selectAboutTheConversion() 
    {
        cy.selectAboutTheConversion()
    }

    static yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible() 
    {
        cy.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible()
    }

    static selectFurtherInformation()
    {
        cy.selectFurtherInformation()
    }

    static yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()
    {
        cy.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()
    }

    static selectFinances()
    {
        cy.selectFinances()
    }

}