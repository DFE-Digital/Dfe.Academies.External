import 'cypress-file-upload'

class AdditionalDetailsDetails {
  public fillAdditionalDetailsDetailsAndSubmit(): this {
    const schoolContribution = 'What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore '
    cy.get('#TrustBenefitDetails').type(schoolContribution)

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

    const docxFilepath = '../fixtures/fifty-k.docx'
    cy.get('#dioceseFileUpload').attachFile(docxFilepath)

    cy.get('#federationOptionNo').click()
    cy.get('#federationOptionNo').should('be.checked')

    cy.get('#supportedByFoundationTrustOrBodyOptionYes').click()

    cy.get('#FoundationTrustOrBodyName').type('Mr Body')

    const pptxFilepath = '../fixtures/fifty-k.pptx'
    cy.get('#foundationConsentFileUpload').attachFile(pptxFilepath)

    cy.get('#exemptionFromSACREOptionNo').click()
    cy.get('#exemptionFromSACREOptionNo').should('be.checked')

    const feederSchools = 'Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do '
    cy.get('#MainFeederSchools').type(feederSchools)

    const pdfFilepath = '../fixtures/fiftyk.pdf'
    cy.get('#resolutionConsentFileUpload').attachFile(pdfFilepath)

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
