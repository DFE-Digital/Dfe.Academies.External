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
        cy.contains('About the conversion').click()
    }

    static yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible() 
    {
        cy.yourApplicationTrustSectionAndAboutConversionCompleteElementsVisible()
    }

    static selectFurtherInformation()
    {
        cy.contains('Further information').click()
    }

    static yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()
    {
        cy.yourApplicationTrustSectionAboutConversionFurtherInformationCompleteElementsVisible()
    }

    static selectFinances()
    {
        cy.contains('Finances').click()
    }

    static financeCompleteElementsVisible()
    {
        cy.financeCompleteElementsVisible()
    }

    static selectFuturePupilNumbers()
    {
        cy.selectFuturePupilNumbers()
    }

    static futurePupilNumbersCompleteElementsVisible()
    {
        cy.futurePupilNumbersCompleteElementsVisible()
    }

    static selectLandAndBuildings()
    {
        cy.selectLandAndBuildings()
    }

    static landAndBuildingsCompleteElementsVisible()
    {
        cy.landAndBuildingsCompleteElementsVisible()
    }

    static selectConsultation()
    {
        cy.selectConsultation()
    }

    static consultationCompleteElementsVisible()
    {
        cy.consultationCompleteElementsVisible()
    }

    static selectPreopeningSupportGrant()
    {
        cy.selectPreopeningSupportGrant()
    }

    static preopeningSupportGrantCompleteElementsVisible()
    {
        cy.preopeningSupportGrantCompleteElementsVisible()
    }

    static selectDeclaration()
    {
        cy.contains('Declaration').click()
    }

    static declarationCompleteElementsVisible()
    {
        cy.declarationCompleteElementsVisible()
    }

    static submitApplication()
    {
        cy.get('input[type="submit"]').click()
    }

    static selectCancelApplication()
    {
        cy.contains('Cancel application').click()
    }

}