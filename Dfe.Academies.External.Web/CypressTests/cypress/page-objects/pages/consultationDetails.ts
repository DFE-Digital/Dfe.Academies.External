class ConsultationDetails {
  public consultationDetailsElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Consultation')

    cy.get('.govuk-body').eq(0).contains('Schools must consult any stakeholders relevant to the conversion')

    cy.get('#role-hint').contains('Has the governing body consulted the relevant stakeholders?')

    cy.get('#consultationStakeholdersOptionYes').should('not.be.checked')
    cy.get('label[for=consultationStakeholdersOptionYes]').contains('Yes')

    cy.get('#consultationStakeholdersOptionNo').should('not.be.checked')
    cy.get('label[for=consultationStakeholdersOptionNo]').contains('No')

    cy.get('label[for=SchoolConsultationStakeholdersConsult]').contains('When does the governing body plan to consult?')
    cy.get('#SchoolConsultationStakeholdersConsult').should('be.enabled')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')

    return this
  }

  public selectHasGovBodyConsultedStakeholdersOptionNo(): this {
    cy.get('#consultationStakeholdersOptionNo').click()
    cy.get('#consultationStakeholdersOptionNo').should('be.checked')

    return this
  }

  public fillConsultationDetails(): this {
    const consultationDetails = 'When does the governing body plan to consult?'
    cy.get('#SchoolConsultationStakeholdersConsult').type(consultationDetails)

    return this
  }

  public submitConsultationDetails(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const consultationDetails = new ConsultationDetails()

export default consultationDetails
