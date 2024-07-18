import 'cypress-file-upload'

class AdditionalDetailsDetails {
  public enterAdditionalDetails(): this {
    cy.get('#TrustBenefitDetails').type('What will the school bring to the trust they are joining?')

    cy.get('#ofstedInspectedOptionNo').click()
    cy.get('#ofstedInspectedOptionNo').should('be.checked')

    cy.get('#safeguardingOptionNo').click()
    cy.get('#safeguardingOptionNo').should('be.checked')

    cy.get('#localAuthorityOptionNo').click()
    cy.get('#localAuthorityOptionNo').should('be.checked')

    cy.get('#localAuthorityClosurePlanOptionNo').click()
    cy.get('#localAuthorityClosurePlanOptionNo').should('be.checked')

    cy.get('#dioceseOptionYes').click()

    cy.get('#DioceseName').type('Mr Diocese')

    cy.get('#dioceseFileUpload').attachFile('../fixtures/fifty-k.docx')

    cy.get('#federationOptionNo').click()
    cy.get('#federationOptionNo').should('be.checked')

    cy.get('#supportedByFoundationTrustOrBodyOptionYes').click()

    cy.get('#FoundationTrustOrBodyName').type('Mr Body')

    cy.get('#foundationConsentFileUpload').attachFile('../fixtures/fifty-k.pptx')

    cy.get('#exemptionFromSACREOptionNo').click()
    cy.get('#exemptionFromSACREOptionNo').should('be.checked')

    cy.get('#MainFeederSchools').type('Please provide a list of your main feeder schools')

    cy.get('#resolutionConsentFileUpload').attachFile('../fixtures/fiftyk.pdf')

    cy.get('#equalitiesImpactAssessmentOptionNo').click()
    cy.get('#equalitiesImpactAssessmentOptionNo').should('be.checked')

    cy.get('#furtherInformationOptionNo').click()
    cy.get('#furtherInformationOptionNo').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const additionalDetailsDetails = new AdditionalDetailsDetails()

export default additionalDetailsDetails
