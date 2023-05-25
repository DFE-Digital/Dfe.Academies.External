
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

    static selectOfstedReportOptionNo()
    {
        cy.get('#ofstedInspectedOptionNo').click()
        cy.get('#ofstedInspectedOptionNo').should('be.checked')
    }

    static selectSafeguardingInvestigationsOptionNo()
    {
        cy.get('#safeguardingOptionNo').click()
        cy.get('#safeguardingOptionNo').should('be.checked')
    }

    static selectSchoolLocalAuthorityReorgOptionNo()
    {
        cy.get('#localAuthorityOptionNo').click()
        cy.get('#localAuthorityOptionNo').should('be.checked')
    }

    static selectSchoolLocalAuthorityClosureOptionNo()
    {
        cy.get('#localAuthorityClosurePlanOptionNo').click()
        cy.get('#localAuthorityClosurePlanOptionNo').should('be.checked')
    }


    static selectYesIsSchoolLinkedToDiocese()
    {
        cy.get('#dioceseOptionYes').click()
    }

    static inputDioceseName()
    {
        cy.get('#DioceseName').type('Mr Diocese')
    }

    static dioceseFileUpload()
    {
        cy.dioceseFileUpload()
    }

    static selectSchoolPartOfFedOptionNo()
    {
        cy.get('#federationOptionNo').click()
        cy.get('#federationOptionNo').should('be.checked')

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

    static selectSchoolExFromChristianWorshipOptionNo()
    {
        cy.get('#exemptionFromSACREOptionNo').click()
        cy.get('#exemptionFromSACREOptionNo').should('be.checked')
    }
    static inputListOfFeederSchools()
    {
        cy.inputListOfFeederSchools()
    }

    static uploadSchoolLetterOfConsent()
    {
        cy.uploadSchoolLetterOfConsent()
	}

    static selectEqualitiesImpactCarriedOutOptionNo()
    {
        cy.get('#equalitiesImpactAssessmentOptionNo').click()
        cy.get('#equalitiesImpactAssessmentOptionNo').should('be.checked')
    }

    static selectAddFurtherInfoOptionNo()
    {
        cy.get('#furtherInformationOptionNo').click()
        cy.get('#furtherInformationOptionNo').should('be.checked')
    }


    static submitAdditionalDetailsDetails()
    {
        cy.get('input[type="submit"]').click()
    }

    




}
