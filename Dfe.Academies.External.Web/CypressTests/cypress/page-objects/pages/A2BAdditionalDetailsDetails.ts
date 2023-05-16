
import BasePage from "../BasePage"
export default class A2BAdditionalDetailsDetails extends BasePage {
    static additionalDetailsDetailsNotStartedElementsVisible()
    {
        cy.additionalDetailsDetailsNotStartedElementsVisible()
    }

    static fillSchoolContribution()
    {
        cy.fillSchoolContribution()
    }

    static selectYesIsSchoolLinkedToDiocese()
    {
        cy.get('#dioceseOptionYes').click()
    }

    static dioceseSectionElementsVisible()
    {
        cy.dioceseSectionElementsVisible()
    }

    static inputDioceseName()
    {
        cy.get('#DioceseName').type('Mr Diocese')
    }

    static dioceseFileUpload()
    {
        cy.dioceseFileUpload()
    }

    static selectYesSchoolSupportedByTrustOrFoundation()
    {
        cy.selectYesSchoolSupportedByTrustOrFoundation()
    }

    static schoolSupportedByElementsVisible()
    {
        cy.schoolSupportedByElementsVisible()
    }

    static inputBodyName()
    {
        cy.get('#FoundationTrustOrBodyName').type('Mr Body')
    }

    static uploadSchoolSupportedByTrustOrBody()
    {
        cy.uploadSchoolSupportedByTrustOrBody()
    }

    static inputListOfFeederSchools()
    {
        cy.inputListOfFeederSchools()
    }

    static uploadSchoolLetterOfConsent()
    {
        cy.uploadSchoolLetterOfConsent()
    }

    static submitAdditionalDetailsDetails()
    {
        cy.get('input[type="submit"]').click()
    }

    




}
