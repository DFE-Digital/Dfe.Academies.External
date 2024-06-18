class ConsultationDetails {
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
