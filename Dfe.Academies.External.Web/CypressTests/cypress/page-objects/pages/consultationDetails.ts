class ConsultationDetails {
  public consultationDetailsElementsVisible(): this {
    cy.consultationDetailsElementsVisible()

    return this
  }

  public selectHasGovBodyConsultedStakeholdersOptionNo(): this {
    cy.get('#consultationStakeholdersOptionNo').click()
    cy.get('#consultationStakeholdersOptionNo').should('be.checked')

    return this
  }

  public fillConsultationDetails(): this {
    cy.fillConsultationDetails()

    return this
  }

  public submitConsultationDetails(): this {
    cy.submitConsultationDetails()

    return this
  }
}

const consultationDetails = new ConsultationDetails()

export default consultationDetails
