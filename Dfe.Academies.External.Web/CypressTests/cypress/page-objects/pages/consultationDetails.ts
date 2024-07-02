class ConsultationDetails {
  public enterConsultationDetails(): this {
    cy.get('#consultationStakeholdersOptionNo').click()
    cy.get('#consultationStakeholdersOptionNo').should('be.checked')

    cy.get('#SchoolConsultationStakeholdersConsult').type('When does the governing body plan to consult?')

    cy.get('input[type=submit]').click()

    return this
  }
}

const consultationDetails = new ConsultationDetails()

export default consultationDetails
