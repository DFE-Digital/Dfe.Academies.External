class AdditionalDetailsDetails {
  public additionalDetailsDetailsNotStartedElementsVisible(): this {
    cy.additionalDetailsDetailsNotStartedElementsVisible()

    return this
  }

  public fillAdditionalDetailsDetailsAndSubmit(): this {
    cy.fillSchoolContribution()
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

    cy.dioceseFileUpload()

    cy.get('#federationOptionNo').click()
    cy.get('#federationOptionNo').should('be.checked')

    cy.selectYesSchoolSupportedByTrustOrFoundation()

    cy.get('#FoundationTrustOrBodyName').type('Mr Body')

    cy.uploadSchoolSupportedByTrustOrBody()

    cy.get('#exemptionFromSACREOptionNo').click()
    cy.get('#exemptionFromSACREOptionNo').should('be.checked')

    cy.inputListOfFeederSchools()

    cy.uploadSchoolLetterOfConsent()

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
