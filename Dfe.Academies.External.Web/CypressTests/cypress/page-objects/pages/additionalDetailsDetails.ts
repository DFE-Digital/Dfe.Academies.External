import 'cypress-file-upload'

class AdditionalDetailsDetails {
  public additionalDetailsDetailsNotStartedElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Additional details')

    cy.get('label[for=TrustBenefitDetails]').contains('What will the school bring to the trust they are joining?')
    cy.get('#trust-benefit-hint').contains('Describe the contribution they will make')

    cy.get('#TrustBenefitDetails').should('be.enabled')

    cy.get('legend').eq(0).contains('Have Ofsted recently inspected the school but not published the report yet?')

    cy.get('#ofstedInspectedOptionYes').should('not.be.checked')
    cy.get('label[for=ofstedInspectedOptionYes]').contains('Yes')

    cy.get('#ofstedInspectedOptionNo').should('not.be.checked')
    cy.get('label[for=ofstedInspectedOptionNo]').contains('No')

    cy.get('legend').eq(1).contains('Are there any safeguarding investigations ongoing at the school')

    cy.get('#safeguardingOptionYes').should('not.be.checked')
    cy.get('label[for=safeguardingOptionYes]').contains('Yes')

    cy.get('#safeguardingOptionNo').should('not.be.checked')
    cy.get('label[for=safeguardingOptionNo]').contains('No')

    cy.get('legend').eq(2).contains('Is the school part of a local authority reorganisation?')

    cy.get('#localAuthorityOptionYes').should('not.be.checked')
    cy.get('label[for=localAuthorityOptionYes]').contains('Yes')

    cy.get('#localAuthorityOptionNo').should('not.be.checked')
    cy.get('label[for=localAuthorityOptionNo]').contains('No')

    cy.get('legend').eq(3).contains('Is the school part of any local authority closure plans?')

    cy.get('#localAuthorityClosurePlanOptionYes').should('not.be.checked')
    cy.get('label[for=localAuthorityClosurePlanOptionYes]').contains('Yes')

    cy.get('#localAuthorityClosurePlanOptionNo').should('not.be.checked')
    cy.get('label[for=localAuthorityClosurePlanOptionNo]').contains('No')

    cy.get('legend').eq(4).contains('Is the school linked to a diocese?')

    cy.get('#dioceseOptionYes').should('not.be.checked')
    cy.get('label[for=dioceseOptionYes]').contains('Yes')

    cy.get('#dioceseOptionNo').should('not.be.checked')
    cy.get('label[for=dioceseOptionNo]').contains('No')

    cy.get('legend').eq(6).contains('Is the school part of a federation')
    cy.get('#federation-hint').contains('A federation is a group of maintained schools under one governing body (The School Governance (Federations) (England) Regulations 2012)')
    cy.get('a[href="https://www.legislation.gov.uk/uksi/2012/1035/contents/made"]').contains('(The School Governance (Federations) (England) Regulations 2012)')

    cy.get('#federationOptionYes').should('not.be.checked')
    cy.get('label[for=federationOptionYes]').contains('Yes')

    cy.get('#federationOptionNo').should('not.be.checked')
    cy.get('label[for=federationOptionNo]').contains('No')

    cy.get('legend').eq(7).contains('Is the school supported by a foundation, trust or other body(e.g. parish council) that appoints foundation governors?')

    cy.get('#supportedByFoundationTrustOrBodyOptionYes').should('not.be.checked')
    cy.get('label[for=supportedByFoundationTrustOrBodyOptionYes]').contains('Yes')

    cy.get('#supportedByFoundationTrustOrBodyOptionNo').should('not.be.checked')
    cy.get('label[for=supportedByFoundationTrustOrBodyOptionNo]').contains('No')

    cy.get('legend').eq(9).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')

    cy.get('#exemptionFromSACREOptionYes').should('not.be.checked')
    cy.get('label[for=exemptionFromSACREOptionYes]').contains('Yes')

    cy.get('#exemptionFromSACREOptionNo').should('not.be.checked')
    cy.get('label[for=exemptionFromSACREOptionNo]').contains('No')

    cy.get('label[for=MainFeederSchools]').contains('Please provide a list of your main feeder schools')
    cy.get('#feeder-schools-hint').contains('We recognise you may have many feeder schools, therefore please just detail the top 5')
    cy.get('#MainFeederSchools').should('be.enabled')

    // SCHOOL'S GOVERNING BODY RESOLUTION UPLOAD QUESTION
    cy.get('legend').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('#Upload-resolutionConsentFiles-hint').contains('This is normally in the form of the minutes from the Governing Body meeting at which the resolution to convert was passed')
    cy.get('label[for=resolutionConsentFileUpload]').contains('Upload a file')

    cy.get('legend').eq(11).contains('Uploaded files')
    cy.get('hr').eq(4).should('be.visible')
    cy.get('.govuk-label').contains('No file uploaded')
    cy.get('hr').eq(5).should('be.visible')

    cy.get('p[class="govuk-body govuk-radios__conditional"]').contains('The application cannot be submitted without this. You can carry on the application and come back to this later.')
    // END OF BODY RESOLUTION FILE UPLOAD BLOCK

    cy.get('legend').eq(12).contains('Has an equalities impact assessment been carried out and considered by the governing body?')

    cy.get('#equalitiesImpactAssessmentOptionYes').should('not.be.checked')
    cy.get('label[for=equalitiesImpactAssessmentOptionYes]').contains('Yes')

    cy.get('#equalitiesImpactAssessmentOptionNo').should('not.be.checked')
    cy.get('label[for=equalitiesImpactAssessmentOptionNo]').contains('No')

    cy.get('legend').eq(13).contains('Do you want to add any further information?')

    cy.get('#furtherInformationOptionYes').should('not.be.checked')
    cy.get('label[for=furtherInformationOptionYes]').contains('Yes')

    cy.get('#furtherInformationOptionNo').should('not.be.checked')
    cy.get('label[for=furtherInformationOptionNo]').contains('No')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')

    return this
  }

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
